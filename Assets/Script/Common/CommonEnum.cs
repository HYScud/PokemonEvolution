/******************玩家枚举**********************/
enum PlayerActionStateEnum
{
    Normal,
    Run,
    Bicycle,
    WaterWalk,
}

enum PlayerStateEnum
{
    Normal,
    Busy,
    Battle,
    Animing,
    Dialog,
}
/******************玩家枚举End**********************/

/******************战斗枚举**********************/
public enum BattleTypeEnum
{
    Wild,
    PVP,
    PVE,
    Boss,
    Plot,
    Champion,
}
public enum BattleNumTypeEnum
{
    Single,
    Double,
    Three,
    Multiple,
}

public enum ActionTypeEnum
{
    Battle,
    UseItem,
    ReplacePokemon,
    RunAway,
}
public enum BattleStateTypeEnum
{
    None,
    Start,
    PlayerAction,
    EnemyAction,
    RunAction,
    Busy,
}
public enum BattleInputEnum
{
    None = 0,
    Next = 1,
    Prev = -1,
    Confirm = 2,

}

public enum BattleWeatherTypeEnum
{
    None,
    Rain,
    Heavy_Rain,
    Sun,
    Harsh_Sunlight,
    Extremely_Harsh_Sunlight,
    Hail,
    Snow,
    Sandstorm,
    Strong_Winds,
    Fog,
    Strong_Sunlight,
}
public enum BattleTerrainTypeEnum 
{
    None = 0,
    Psychic=1, 
    Misty,
    Grassy,
}
public enum MoveResultEnum
{
    Effective,
    SuperEffective,
    NotVeryEffective,
    NotEffective,
    Effective_CriticalHit,
    SuperEffective_CriticalHit,
    NotVeryEffective_CriticalHit
}
/******************战斗枚举End**********************/
public enum UITypeColorEnum
{
    SYMBOL,
    TYPE_NAME,
    ATTACK,
}
/******************宝可梦枚举**********************/
public enum PokemonTypeEnum
{
    Normal,
    Alpha,
}

public enum ExpTypeEnum
{
    Best_Fast,
    Faster,
    Fast,
    Slow,
    Slower,
    Best_Slow,
}

public enum SexTypeEnum
{
    None,
    Male,
    Female,
    UnKnown
}

public enum TypeEnum
{
    None,//无属性
    Normal,//一般
    Fighting,//格斗系
    Flying,//飞行系
    Poison,//毒系
    Ground,//地系
    Rock,//岩石系
    Bug,//虫系
    Ghost,//幽灵系
    Steel,//钢系
    Fire,//火系
    Water,//水系
    Grass,//草系
    Electric,//电系
    Psychic,//超能力
    Ice,//冰系
    Dragon,//龙系
    Dark,//恶系
    Fairy,//妖精系
}

public enum NatrueTypeEnum
{
    Hardy,//勤奋
    Lonely,//怕寂寞
    Adamant,//固执
    Naughty,//顽皮
    Brave,//勇敢
    Bold,//大胆
    Docile,//坦率
    Impish,//淘气
    Lax,//乐天
    Relaxed,//悠闲
    Modest,//内敛
    Mild,//慢吞吞
    Bashful,//害羞
    Rash,//马虎
    Quiet,//冷静
    Calm,//温和
    Gentle,//温顺
    Careful,//慎重
    Quirky,//浮躁
    Sassy,//自大
    Timid,//胆小
    Hasty,//急躁
    Jolly,//爽朗
    Naive,//天真
    Serious,//认真

}
public enum ShinyTypeEnum
{
    None,
    Square,
    Star,
    Special
}

public enum CatchTypeEnum
{
    None,
    Wild,
    Egg,
    Gift
}

public enum StatusTypeEnum
{
    brn,
    frz,
    hyp,
    par,
    psn,
    slp
}
/******************宝可梦枚举End**********************/

/******************技能枚举**********************/
public enum MoveTypeEnum
{
    Special_Move,
    Physical_Move,
    Status_Move
}
public enum MoveATKRangeEnum
{
    None,
    Single,
    Multiple,
    All_Enemy,
    All
}

public enum MoveContactEnum
{
    None,
    Direct_Attack,
    Special_Attack,
}
public enum MoveLearnedTypeEnum
{
    None,
    Level,
    Hidden_Machine,
    Technical_Machine,
    Teach,
    Inherit,
}
/******************技能枚举End**********************/