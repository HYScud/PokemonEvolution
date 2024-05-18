using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    /*[SerializeField] List<BattleUnit> PlayerUnitList;
    [SerializeField] List<BattleUnit> EnemyUnitList;
    [SerializeField] List<BattleHud> PlayerHudList;
    [SerializeField] List<BattleHud> EnemyHudList;*/
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialog dialogText;

    private GameObject BattleDialogSW;
    private GameObject BattleDialog;
    private GameObject BattleMoveBox;
    private GameObject BattleActionUISw;
    private GameObject BattleEnemyHud;
    private GameObject BattlePlayerHud;

    private Pokemon CurActionPokemon;

    private List<BattleAction> AllAction;
    void Start()
    {
        InitBattle();
    }
    public void InitBattle()
    {
        playerUnit.Init();
        enemyUnit.Init();
        playerHud.SetPokemonInfo(playerUnit.PokemonUnit);
        enemyHud.SetPokemonInfo(enemyUnit.PokemonUnit);
        InitPanel();
    }

    public void InitPanel()
    {
        BattleDialogSW = (transform.Find("BattleCanvas/BattleDialogSW") as Transform).gameObject;
        BattleDialog = (transform.Find("BattleCanvas/BattleDialog") as Transform).gameObject;
        BattleMoveBox = (transform.Find("BattleCanvas/BattleMoveBox") as Transform).gameObject;
        BattleActionUISw = (transform.Find("BattleCanvas/BattleActionUISw") as Transform).gameObject;
        BattleEnemyHud = (transform.Find("BattleCanvas/BattleEnemyHud") as Transform).gameObject;
        BattlePlayerHud = (transform.Find("BattleCanvas/BattleOwnHud") as Transform).gameObject;
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
    }

    public void TransitionForEndBattle()
    {

    }
    public void OnClickBattle()
    {
        SwitchPanelShowOrHiden(BattleActionUISw, false);
        SwitchPanelShowOrHiden(BattleMoveBox, true);
        MoveBoxInit();
    }
    public void MoveBoxInit()
    {
        var btnNum = BattleMoveBox.transform.childCount;
        Debug.Log("按钮数量" + btnNum);
        var moveList = CurActionPokemon.GetCurMove();
        for (int i = 0; i < Mathf.Max(moveList.Count, btnNum); i++)
        {
            var btn = BattleMoveBox.transform.GetChild(i);
            if (i < moveList.Count)
            {
                btn.gameObject.SetActive(true);
                var bgImage = btn.GetComponent<Image>();
                var iconImage = btn.Find("Image").GetComponent<Image>();
                var InfoBox = (btn.Find("InfoBox") as Transform).gameObject;
                var PP_Text = (btn.Find("PP_Text") as Transform).gameObject.GetComponent<TextMeshProUGUI>();

                if (i < btnNum - 1)
                {
                    if (bgImage != null)
                    {
                        Color nowColor;
                        string colorStr = PokemonTable.GetTypeColorEffect((int)moveList[i].MoveBase.MoveType - 1, UITypeColorEnum.SYMBOL);
                        UnityEngine.ColorUtility.TryParseHtmlString(colorStr, out nowColor);
                        bgImage.color = nowColor;
                    }
                    if (iconImage != null)
                    {
                        iconImage.sprite = Resources.Load<Sprite>("UI/BattleUI/DIYUI/Icon/Pokemon_Type_Icon_" + moveList[i].MoveBase.MoveType.ToString());
                    }
                    if (InfoBox != null)
                    {
                        var parennt = InfoBox.transform.parent;
                        Debug.Log("parennt " + InfoBox.transform.parent.name);
                        var childList = InfoBox.GetComponentsInChildren<TextMeshProUGUI>(true);
                        var moveNameText = childList[0];
                        var EffectText = childList[1];
                        moveNameText.text = moveList[i].MoveBase.MoveName;
                        var templist = new List<string> { "有效果", "效果拔群", "无效果", "" };
                        string check = templist[Random.Range(1, 4)];
                        EffectText.text = check;
                        EffectText.gameObject.SetActive(check != string.Empty);
                    }
                    if (PP_Text != null)
                    {
                        PP_Text.text = $"{moveList[i].CurPP}/{moveList[i].MoveBase.InitialPP}";
                    }
                    btn.GetComponent<Button>().onClick.RemoveAllListeners();
                    btn.GetComponent<Button>().onClick.AddListener(() => ToUseMove(moveList[i]));
                }
            }
            else
            {
                if (i < btnNum - 1 && btn != null)
                    btn.gameObject.SetActive(false);
            }
        }
    }
    public void ToUseMove(Move move)
    {
        AddActionToList(move, ShowSelect(move.MoveBase.MoveATKRangeEnum));
    }

    public Pokemon ShowSelect(MoveATKRangeEnum moveATKRangeEnum)
    {
        /*TODO:ShowSelectPokemon*/
        return enemyUnit.PokemonUnit;
    }
    public void AddActionToList(Move move,Pokemon targetPokemon)
    {
        AllAction.Add(new BattleAction(CurActionPokemon, targetPokemon, ActionTypeEnum.Battle));
    }
    public void BackToActionUI() {
        SwitchPanelShowOrHiden(dialogText.gameObject, false);
        SwitchPanelShowOrHiden(BattleActionUISw, true);
        SwitchPanelShowOrHiden(BattleMoveBox, false);
    }

    public void SwitchPanelShowOrHiden<T>(T t, bool bIsShow)
    {
        var TTransform = t as GameObject;
        if (TTransform != null)
        {
            Debug.Log("SwitchPanelShowOrHiden succ");
            TTransform.gameObject.SetActive(bIsShow);
        }
        else
        {
            Debug.Log("BattleSystem TTransform is null");
        }
    }

    void Update()
    {

    }
}
