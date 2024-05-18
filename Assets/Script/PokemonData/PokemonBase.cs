using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "CreateDataBase/pokemon")]
public class PokemonBase : ScriptableObject
{
    //基本信息
    [SerializeField] int pokemonId;//宝可梦编号—>同一宝可梦相同
    [SerializeField] string pokemonName;
    [SerializeField] TypeEnum pokemonType1 = TypeEnum.None;
    [SerializeField] TypeEnum pokemonType2 = TypeEnum.None;
    [TextArea]
    [SerializeField] string pokemonDesc;
    [SerializeField] double maleRatio = 5.0d;
    [SerializeField] int catchRatio = 75;
    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;
    [SerializeField] ExpTypeEnum expType=ExpTypeEnum.Slow;
    //基本属性
    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defence;
    [SerializeField] int specialATK;
    [SerializeField] int specialDef;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> allLearnableMoves;

    public int PokemonId { get => pokemonId; }
    public string PokemonName { get => pokemonName; }
    public TypeEnum PokemonType1 { get => pokemonType1; }
    public TypeEnum PokemonType2 { get => pokemonType2; }
    public string PokemonDesc { get => pokemonDesc; }
    public double MaleRatio { get => maleRatio; }
    public int CatchRatio { get => catchRatio; }
    public int Hp { get => hp; }
    public int Attack { get => attack; }
    public int SpecialATK { get => specialATK; }
    public int Defence { get => defence; }
    public int SpecialDef { get => specialDef; }
    public int Speed { get => speed; }

    //技能相关（当前宝可梦所有技能）
    public List<LearnableMove> AllLearnableMoves { get => allLearnableMoves; }
    public Sprite FrontSprite { get => frontSprite; set => frontSprite = value; }
    public Sprite BackSprite { get => backSprite; set => backSprite = value; }
    public ExpTypeEnum ExpType { get => expType;}
}
