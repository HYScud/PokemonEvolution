using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class BattleMoveBox : MonoBehaviour
{
    private int curSelect = 0;
    private Transform SelectBtn = null;
    private List<Transform> AllActiveBtns = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveBoxInit(List<Move> moveList)
    {
        AllActiveBtns.Clear();
        var btnNum = transform.childCount;
        Debug.Log("按钮数量" + btnNum);
        for (int i = 0; i < Mathf.Max(moveList.Count, btnNum); i++)
        {
            var btn = transform.GetChild(i);
            btn.GetComponent<Button>().onClick.RemoveAllListeners();
            if (i < moveList.Count)
            {
                AllActiveBtns.Add(btn);
                if (SelectBtn != null)
                {
                    SelectBtn = btn;
                }
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
                    if (i < moveList.Count)
                    {
                        var index = i;
                        btn.GetComponent<Button>().onClick.AddListener(() => { ToUseMove(moveList[index]); });
                    }
                }
            }
            else
            {
                if (i < btnNum - 1 && btn != null)
                    btn.gameObject.SetActive(false);
                else
                {
                    btn.GetComponent<Button>().onClick.RemoveAllListeners();
                    btn.GetComponent<Button>().onClick.AddListener(() => BackToActionUI());
                    AllActiveBtns.Add(btn);
                }
            }
        }
        this.RefreshBtn();
    }
    public void RefreshBtn()
    {
        /*if (curSelect >= 0 && curSelect < AllActiveBtns.Count)
        {
            AllActiveBtns[curSelect].Find("Seleced").gameObject.SetActive(true);
        }*/
        for (int i = 0; i < AllActiveBtns.Count; i++)
        {
            if (curSelect == i)
                AllActiveBtns[i].Find("Seleced").gameObject.SetActive(true);
            else
                AllActiveBtns[i].Find("Seleced").gameObject.SetActive(false);
        }
    }
    public void ChangeSelectedBtn(int detal)
    {
        var index = Mathf.Abs(curSelect + detal) % AllActiveBtns.Count;
        curSelect = index;
        Debug.Log("打印curselect"+curSelect);
        RefreshBtn();
    }
    public void ExecuteInput(BattleInputEnum inputAction)
    {
        if (inputAction == BattleInputEnum.Confirm)
        {
            var btn = AllActiveBtns[curSelect];
            if (btn != null)
            {
                Debug.Log("打印"+ btn.name);
                btn.GetComponent<Button>().onClick.Invoke();
            }
        }
        else
        {
            ChangeSelectedBtn((int)inputAction);
        }
    }
    public void ToUseMove(Move move)
    {
        BattleEventData eventData = new BattleEventData(move, EventID.UseMove);
        eventData.Send();
    }
    public void BackToActionUI()
    {
        EventData.CreateEvent(EventID.BackToBattleAction).Send();
    }
}
