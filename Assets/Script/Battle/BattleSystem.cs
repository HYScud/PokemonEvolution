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

    private float timeDet = 0;

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
            AllAction.Add(new BattleAction(playerUnit.PokemonUnit, enemyUnit.PokemonUnit, move, ActionTypeEnum.Battle));
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
            AllAction.Add(new BattleAction(enemyUnit.PokemonUnit, playerUnit.PokemonUnit, move, ActionTypeEnum.Battle));
            CheckForAcitionEnd();
        }
    }

    public Pokemon ShowSelect(MoveATKRangeEnum moveATKRangeEnum)
    {
        /*TODO:ShowSelectPokemon*/
        return enemyUnit.PokemonUnit;
    }

    public void AddActionToList(Move move, Pokemon targetPokemon)
    {
        AllAction.Add(new BattleAction(CurActionPokemon, targetPokemon, ActionTypeEnum.Battle));
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
        //重构排序逻辑
        AllAction.Sort((next, prev) =>
        {
            return next.SourcePokemon.Speed - prev.SourcePokemon.Speed;
        });
        RunAction();
    }

    public void RunAction()
    {

    }

    public void BackToActionUI()
    {
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
            Debug.Log("BattleSystem TTransform is null");
        }
    }
}
