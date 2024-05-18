
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
    // Start is called before the first frame update
    void Start()
    {

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
            if (HpBar != null)
            {
                HpBar.size = pokemon.CurHp / pokemon.MaxHp;
            }
            if (HPText != null)
            {
                HPText.text = string.Format("{0}/{1}", pokemon.CurHp, pokemon.MaxHp);
            }
        }
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
            }
        }
    }
}
