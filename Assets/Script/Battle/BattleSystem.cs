using Cysharp.Threading.Tasks;
using NPOI.SS.Formula.Eval;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour, EventObserver
{
    private BattleWeatherTypeEnum weatherTypeEnum = BattleWeatherTypeEnum.None;//天气
    private BattleTerrainTypeEnum battleTerrainTypeEnum = BattleTerrainTypeEnum.None;//场地
    private BattleNumTypeEnum battleNumType;
    private BattleStateTypeEnum battleState = BattleStateTypeEnum.None;
    /*[SerializeField] List<BattleUnit> PlayerUnitList;
    [SerializeField] List<BattleUnit> EnemyUnitList;
    [SerializeField] List<BattleHud> PlayerHudList;
    [SerializeField] List<BattleHud> EnemyHudList;*/
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialog dialogText;
    [SerializeField] BattleMoveBox moveBox;

    private GameObject BattleDialogSW;
    private GameObject BattleDialog;
    private GameObject BattleMoveBox;
    private GameObject BattleActionUISw;
    private GameObject BattleEnemyHud;
    private GameObject BattlePlayerHud;

    private Pokemon CurActionPokemon;

    private List<BattleAction> AllAction;

    private int curSelect = 0;
    private Transform SelectBtn = null;
    [SerializeField] List<Transform> AllActiveBtns = new List<Transform>();

    public BattleStateTypeEnum BattleState { get => battleState; private set => battleState = value; }

    void Start()
    {
        EventManager.Register(this, EventID.UseMove, EventID.BackToBattleAction);
        InitBattle();
    }

    void Update()
    {
        if (battleState == BattleStateTypeEnum.PlayerAction)
        {
            HandleActionSelection();
        }
    }

    public void HandleActionSelection()
    {
        BattleInputEnum inputAction = 0;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputAction = BattleInputEnum.Next;
            Debug.Log("next");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputAction = BattleInputEnum.Prev;
            Debug.Log("prev");
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            inputAction = BattleInputEnum.Confirm;
            Debug.Log("Confir");
        }
        if (inputAction != BattleInputEnum.None)
        {
            //battleState = BattleStateTypeEnum.Busy;
            ChoosePanelToRunInput(inputAction);
        }

    }

    void ChoosePanelToRunInput(BattleInputEnum inputAction)
    {
        if (inputAction == BattleInputEnum.Confirm)
            Debug.Log("ChoosePanelToRunInput");
        bool check = BattleMoveBox.activeInHierarchy;
        Debug.Log("打印bool" + check);
        if (check)
        {
            moveBox.ExecuteInput(inputAction);
        }
        else
        {
            if (inputAction == BattleInputEnum.Confirm)
                Debug.Log("执行当前input");
            ExecuteInput(inputAction);
        }
        /*if (inputAction == BattleInputEnum.Confirm)
            await UniTask.WaitForSeconds(1);
        battleState = BattleStateTypeEnum.PlayerAction;*/
        Debug.Log("执行当前battleState");
    }

    public void RefreshBtn()
    {

        for (int i = 0; i < AllActiveBtns.Count; i++)
        {
            var text = AllActiveBtns[i].GetComponentInChildren<TextMeshProUGUI>();
            var bg = AllActiveBtns[i].GetComponent<Image>();
            if (curSelect == i)
            {
                AllActiveBtns[i].Find("Selected").gameObject.SetActive(true);
            }
            else
            {
                AllActiveBtns[i].Find("Selected").gameObject.SetActive(false);
            }
            if (text != null)
            {
                text.color = curSelect != i ? Color.black : Color.white;
            }
            if (bg != null)
            {
                bg.color = curSelect != i ? Color.white : Color.black;
            }
        }
    }

    void ChangeSelectedBtn(int detal)
    {
        var index = Mathf.Abs(curSelect + detal) % AllActiveBtns.Count;
        curSelect = index;
        Debug.Log("打印curselect" + curSelect);
        RefreshBtn();
    }

    public void ExecuteInput(BattleInputEnum inputAction)
    {
        Debug.Log("执行当前input" + inputAction);
        if (inputAction == BattleInputEnum.Confirm)
        {
            var btn = AllActiveBtns[curSelect];
            if (btn != null)
            {
                btn.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            ChangeSelectedBtn((int)inputAction);
        }
    }

    public void HandleEvent(EventData resp)
    {
        switch (resp.eid)
        {
            case EventID.UseMove:
                var moveInfo = resp as BattleEventData;
                if (moveInfo != null) { this.SelectTarget(moveInfo.SelectedMove); }
                break;
            case EventID.BackToBattleAction:
                BackToActionUI();
                break;
        }
    }
    public void InitBattle()
    {
        AllAction = new List<BattleAction>();
        battleState = BattleStateTypeEnum.Start;
        playerUnit.Init();
        enemyUnit.Init();
        playerHud.SetPokemonInfo(playerUnit.PokemonUnit);
        enemyHud.SetPokemonInfo(enemyUnit.PokemonUnit);
        RefreshBtn();
        InitPanel();
    }

    public void InitPanel()
    {
        BattleDialogSW = (transform.Find("BattleCanvas/BattleDialogSW")).gameObject;
        BattleDialog = (transform.Find("BattleCanvas/BattleDialog")).gameObject;
        BattleMoveBox = transform.Find("BattleCanvas/BattleMoveBox").gameObject;
        BattleActionUISw = (transform.Find("BattleCanvas/BattleActionUISw")).gameObject;
        BattleEnemyHud = (transform.Find("BattleCanvas/BattleEnemyHud")).gameObject;
        BattlePlayerHud = (transform.Find("BattleCanvas/BattleOwnHud")).gameObject;
        if (BattleDialog != null)
        {
            Debug.Log("BattleDialog");
            BattleMoveBox.SetActive(false);
            BattleDialog.SetActive(false);
            BattleActionUISw.SetActive(false);
            BattlePlayerHud.SetActive(false);
        }
        TransitionForStartBattle().Forget();
    }

    public async UniTaskVoid TransitionForStartBattle()
    {
        await dialogText.SetDialogByWord($"野生的{enemyUnit.PokemonUnit.Name}跳了出来");
        SwitchPanelShowOrHiden(dialogText.gameObject, false);
        SwitchPanelShowOrHiden(BattleActionUISw, true);
        SwitchPanelShowOrHiden(BattlePlayerHud, true);
        CurActionPokemon = playerUnit.PokemonUnit;
        RefreshBtn();
        battleState = BattleStateTypeEnum.PlayerAction;
    }

    public void TransitionForEndBattle()
    {

    }

    public void OnClickBattle()
    {
        SwitchPanelShowOrHiden(BattleActionUISw, false);
        SwitchPanelShowOrHiden(BattleMoveBox, true);
        moveBox.MoveBoxInit(CurActionPokemon.GetCurMove());
    }

    public void SelectTarget(Move move)
    {
        Debug.Log("*******选中技能*******" + move.MoveBase.MoveName);
        if (battleNumType == BattleNumTypeEnum.Single)
        {
            //TODO：后续可用AddActionToList方法
            AllAction.Add(new BattleAction(playerUnit.PokemonUnit, enemyUnit.PokemonUnit, move, ActionTypeEnum.Battle, true));
            WildAction();
        }
        else
        {
            Debug.Log("*******暂时没做*******" + move.MoveBase.MoveName);
        }
    }

    public void WildAction()
    {
        var move = enemyUnit.PokemonUnit.WildSelectMove();
        if (move != null)
        {
            //TODO:简化战斗（后续实际按CurActionPokemon为source，且需要全程交替改变CurActionPokemon）
            AllAction.Add(new BattleAction(enemyUnit.PokemonUnit, playerUnit.PokemonUnit, move, ActionTypeEnum.Battle, false));
            CheckForAcitionEnd();
        }
    }

    public Pokemon ShowSelect(MoveATKRangeEnum moveATKRangeEnum)
    {
        /*TODO:ShowSelectPokemon*/
        return enemyUnit.PokemonUnit;
    }

    public void AddActionToList(Move move, Pokemon targetPokemon, bool bIsPlayer)
    {
        AllAction.Add(new BattleAction(CurActionPokemon, targetPokemon, ActionTypeEnum.Battle, bIsPlayer));
    }

    public void CheckForAcitionEnd()
    {

        //TODO:简化战斗实际的阈值为所有战斗中存活的Pokemon
        if (AllAction.Count >= 2)
        {
            OrderAction();
        }
    }

    public void OrderAction()
    {
        Debug.Log("开始对操作进行排序");
        //重构排序逻辑
        AllAction.Sort((next, prev) =>
        {
            return prev.SourcePokemon.Speed - next.SourcePokemon.Speed;
        });
        RunAllAction();
    }

    /*开始battle，执行操作*/
    public void RunAllAction()
    {
        if (AllAction.Count == 0)
        {
            SwitchPanelShowOrHiden(enemyHud.gameObject, true);
            BackToActionUI();
            return;
        }
        Debug.Log("开始执行操作");
        battleState = BattleStateTypeEnum.RunAction;
        var battleAction = AllAction[0];
        switch (battleAction.ActionType)
        {
            case ActionTypeEnum.Battle:
                RunMoveAction(battleAction).Forget();
                break;
            default: break;
        }
    }

    public async UniTaskVoid RunMoveAction(BattleAction battleAction)
    {
        HideAllPanel();
        if (battleAction != null)
        {
            BattleHud targetHud = battleAction.BIsPlayer ? enemyHud : playerHud;
            //BattleHud sourceHud = !battleAction.BIsPlayer ? enemyHud : playerHud;
            SwitchPanelShowOrHiden(dialogText.gameObject, true);
            await dialogText.SetDialogByWord($"{battleAction.SourcePokemon.Name}使用了{battleAction.Move.MoveBase.MoveName}");
            //Player MoveAnimation
            SwitchPanelShowOrHiden(dialogText.gameObject, false);
            if (battleAction.Move.MoveBase.MoveTypeEnum == MoveTypeEnum.Status_Move)
            {
                Debug.Log("使用状态技能");
            }
            else
            {
                SwitchPanelShowOrHiden(targetHud.gameObject, true);
                await UniTask.WaitForSeconds(0.3f);
                MoveResultEnum moveResultEnum = battleAction.TargetPokemon.TakeDamage(battleAction.SourcePokemon, battleAction.Move);
                await targetHud.SetHpSmooth(battleAction.TargetPokemon);
                await UniTask.WaitForSeconds(0.5f);
                SwitchPanelShowOrHiden(targetHud.gameObject, false);
                SwitchPanelShowOrHiden(dialogText.gameObject, true);
                switch (moveResultEnum)
                {
                    case MoveResultEnum.Effective_CriticalHit:
                        await dialogText.SetDialogByWord("命中要害");
                        break;
                    case MoveResultEnum.NotVeryEffective_CriticalHit:
                        await dialogText.SetDialogByWord("命中要害");
                        await dialogText.SetDialogByWord("效果不好");
                        break;
                    case MoveResultEnum.SuperEffective_CriticalHit:
                        await dialogText.SetDialogByWord("命中要害");
                        await dialogText.SetDialogByWord("效果把群");
                        break;
                    case MoveResultEnum.SuperEffective:
                        await dialogText.SetDialogByWord("效果把群");
                        break;
                    case MoveResultEnum.NotVeryEffective:
                        await dialogText.SetDialogByWord("效果不好");
                        break;
                    case MoveResultEnum.NotEffective:
                        await dialogText.SetDialogByWord("没有效果");
                        break;
                    default:
                        await dialogText.SetDialogByWord("有效果");
                        break;
                }
                await UniTask.WaitForSeconds(0.5f);
                SwitchPanelShowOrHiden(dialogText.gameObject, false);
            }
        }
        BattleRoundEndTypeEnum isBattleEnd = CheckForBattleEnd();
        if(isBattleEnd != BattleRoundEndTypeEnum.None)
        {
            BattleEnd();
            return;
        }
        AllAction.Remove(battleAction);
        RunAllAction();
    }
    BattleRoundEndTypeEnum CheckForBattleEnd()
    {
        if(enemyUnit.PokemonUnit.CurHp<=0)
        {
            return BattleRoundEndTypeEnum.Own_Win;
        }
        else if(playerUnit.PokemonUnit.CurHp<=0)
        {
            return BattleRoundEndTypeEnum.Target_Win;
        }
        else
        {
            return BattleRoundEndTypeEnum.None;
        }
    }
    void BattleEnd()
    {
        Debug.Log("战斗结束");
    }
    public void HideAllPanel()
    {
        SwitchPanelShowOrHiden(dialogText.gameObject, false);
        SwitchPanelShowOrHiden(BattleActionUISw, false);
        SwitchPanelShowOrHiden(BattleMoveBox, false);
        SwitchPanelShowOrHiden(playerHud.gameObject, false);
        SwitchPanelShowOrHiden(enemyHud.gameObject, false);
    }

    public void BackToActionUI()
    {
        SwitchPanelShowOrHiden(playerHud.gameObject, true);
        SwitchPanelShowOrHiden(enemyHud.gameObject, true);
        SwitchPanelShowOrHiden(dialogText.gameObject, false);
        SwitchPanelShowOrHiden(BattleActionUISw, true);
        SwitchPanelShowOrHiden(BattleMoveBox, false);
    }

    public void SwitchPanelShowOrHiden<T>(T t, bool bIsShow)
    {
        var TTransform = t as GameObject;
        if (TTransform != null)
        {
            TTransform.gameObject.SetActive(bIsShow);
        }
        else
        {
            Debug.LogFormat("BattleSystem TTransform：{0} is null");
        }
    }
}
