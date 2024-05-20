
using Cysharp.Threading.Tasks;
using NPOI.OpenXmlFormats.Spreadsheet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Scrollbar HpBar;
    [SerializeField] Scrollbar ExpBar;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] TextMeshProUGUI LevelText;
    [SerializeField] TextMeshProUGUI HPText;
    [SerializeField] Image SexImage;
    [SerializeField] Image StatusImage;
    [SerializeField] Image Type1Image;
    [SerializeField] Image Type2Image;
    // Start is called before the first frame update
    void Awake()
    {
        var transfrom = transform.Find("Status") as Transform;
        var type1 = transform.Find("TypeBox/Type1") as Transform;
        var type2 = transform.Find("TypeBox/Type2") as Transform;
        var sexImage = transform.Find("HudBox/InfoBox/SexImage") as Transform;
        if (transfrom != null)
            StatusImage = transfrom.gameObject.GetComponent<Image>();
        else
            Debug.Log("StatusImage is null");
        if (type1 != null)
        {
            Type1Image = type1.gameObject.GetComponent<Image>();
        }
        else
        {
            Debug.Log("Type1Image is null");
        }
        if (type2 != null)
        {
            Type2Image = type2.gameObject.GetComponent<Image>();
        }
        else
        {
            Debug.Log("Type2Image is null");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetPokemonInfo(Pokemon pokemon)
    {
        SetHP(pokemon);
        SetExp(pokemon);
        SetName(pokemon);
        SetLevelText(pokemon);
        SetStatus(pokemon.StatusTypeEnum);
        SetType(pokemon);
        SetPokemonSex(pokemon.SexTypeEnum);
    }

    public void SetLevelText(Pokemon pokemon)
    {
        if (LevelText != null)
        {
            LevelText.text = string.Format("Lv.{0}", pokemon.Level);
        }
    }

    public float GetCurHPRatio()
    {
        if (HpBar != null)
        {
            return HpBar.size > 0 ? (HpBar.size < 1 ? HpBar.size : 1f) : 0f;
        }
        return 0f;
    }

    public void SetHP(Pokemon pokemon)
    {
        if (pokemon != null)
        {
            SetHPBar((float)pokemon.CurHp / pokemon.MaxHp);
            string curHpText = string.Format("{0}/{1}", pokemon.CurHp, pokemon.MaxHp);
            SetHPText(curHpText);
        }
    }

    private void SetHPText(string curHpText)
    {
        if (HPText != null)
        {
            HPText.text = curHpText;
        }
    }

    public void SetHPBar(float HpRatio)
    {
        if (HpBar != null)
        {
            HpBar.size = HpRatio;
        }
    }

    public async UniTask<bool> SetHpSmooth(Pokemon pokemon)
    {
        float newRatio = (float)pokemon.CurHp / pokemon.MaxHp;
        float curRatio = HpBar.size;
        float detalHP = 1f;

        while (curRatio - newRatio > Mathf.Epsilon)
        {
            Debug.LogFormat("´òÓ¡²îÖµ{0}£¬{1}",curRatio - newRatio, Mathf.Epsilon);
            curRatio -= newRatio * detalHP / pokemon.MaxHp;
            SetHPBar(curRatio);
            string curHpText = string.Format("{0}/{1}", Mathf.FloorToInt(curRatio * pokemon.MaxHp), pokemon.MaxHp);
            SetHPText(curHpText);
            await UniTask.WaitForSeconds(1);
        }
        return true;
    }

    public float GetCurEXPRatio()
    {
        if (ExpBar != null)
        {
            return ExpBar.size > 0 ? (ExpBar.size < 1 ? ExpBar.size : 1f) : 0f;
        }
        return 0f;
    }

    public void SetExp(Pokemon pokemon)
    {
        SetExpByExp(pokemon.GetCurExpRatio());
    }

    public void SetExpByExp(float expRatio)
    {
        if (ExpBar != null)
        {
            ExpBar.size = expRatio;
        }
    }

    public void AddExpByExp(float detal)
    {
        if (ExpBar != null)
        {
            ExpBar.size += detal;
        }
    }

    public void SetName(Pokemon pokemon)
    {
        if (NameText != null)
        {
            NameText.text = pokemon.Name;
        }
    }

    public void SetStatus(StatusTypeEnum status)
    {
        if (StatusImage != null)
        {
            string path = "UI/BattleUI/DIYUI/Status/";
            switch (status)
            {
                case StatusTypeEnum.brn:
                    path += "brn";
                    break;
                case StatusTypeEnum.frz:
                    path += "frz";
                    break;
                case StatusTypeEnum.hyp:
                    path += "hyp";
                    break;
                case StatusTypeEnum.par:
                    path += "par";
                    break;
                case StatusTypeEnum.psn:
                    path += "psn";
                    break;
                case StatusTypeEnum.slp:
                    path += "slp";
                    break;
                default:
                    break;
            }
            path += status.ToString();
            Debug.Log(path);
            StatusImage.sprite = Resources.Load<Sprite>(path);
        }
    }

    public void SetPokemonSex(SexTypeEnum sexType)
    {
        if (SexImage != null)
        {
            switch (sexType)
            {
                case SexTypeEnum.UnKnown:
                case SexTypeEnum.None:
                    SexImage.sprite = null;
                    break;
                case SexTypeEnum.Female:
                    SexImage.sprite = Resources.Load<Sprite>("UI/BattleUI/DIYUI/female");
                    break;
                case SexTypeEnum.Male:
                    SexImage.sprite = Resources.Load<Sprite>("UI/BattleUI/DIYUI/male");
                    break;
                default:
                    SexImage.sprite = null;
                    break;
            }
        }
    }

    public void SetType(Pokemon pokemon)
    {
        if (pokemon != null && pokemon.pkBase)
        {
            string path1 = "UI/BattleUI/DIYUI/Icon/Pokemon_Type_Icon_";
            string path2 = "UI/BattleUI/DIYUI/Icon/Pokemon_Type_Icon_";
            if (Type1Image != null)
            {
                path1 += pokemon.pkBase.PokemonType1.ToString();
                Type1Image.sprite = Resources.Load<Sprite>(path1);
            }
            if (Type2Image != null)
            {
                path2 += pokemon.pkBase.PokemonType2.ToString();
                Type2Image.sprite = Resources.Load<Sprite>(path2);
            }


        }
    }
}
