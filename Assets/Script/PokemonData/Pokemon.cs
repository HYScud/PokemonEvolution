using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Pokemon
{
    public PokemonBase pkBase { get; set; }

    //基本信息
    private int level;
    private long pokemonId;//实际的id—>每个宝可梦都不同
    private long ownerId = 0;
    private int catchArea = 0;
    private CatchTypeEnum catchTypeEnum = CatchTypeEnum.None;
    private string nickName = "";
    private int MaxLevel = 100;

    private SexTypeEnum sexTypeEnum = SexTypeEnum.None;
    private NatrueTypeEnum natrueTypeEnum { get; set; }
    private ShinyTypeEnum shinyType = ShinyTypeEnum.None;

    //努力值
    [SerializeField] int BasePoints_HP = 0;
    [SerializeField] int BasePoints_ATk = 0;
    [SerializeField] int BasePoints_SPATk = 0;
    [SerializeField] int BasePoints_Def = 0;
    [SerializeField] int BasePoints_SPDef = 0;
    [SerializeField] int BasePoints_Speed = 0;

    //亲密度-美丽度等
    [SerializeField] int BasePoints_Friendship = 0;
    [SerializeField] int BasePoints_Beauty = 0;
    [SerializeField] int BasePoints_Cute = 0;
    [SerializeField] int BasePoints_Smart = 0;
    [SerializeField] int BasePoints_Cool = 0;
    [SerializeField] int BasePoints_Tough = 0;

    //基本属性-个体值
    [SerializeField] int IV_Hp = 0;
    [SerializeField] int IV_Attack = 0;
    [SerializeField] int IV_Defence = 0;
    [SerializeField] int IV_SpecialATK = 0;
    [SerializeField] int IV_SpecialDef = 0;
    [SerializeField] int IV_Speed = 0;

    [SerializeField] StatusTypeEnum statusTypeEnum;

    [SerializeField] int criticalHit = 0;
    public string Name
    {
        get { if (nickName == null || nickName == "") return pkBase.PokemonName; else return nickName; }
    }
    //战斗属性
    public int CurHp { get; set; }

    public int CurTotalExp { get; set; } = 0;

    public int CurExp { get; set; }

    public int MaxHp { get { if (pkBase.PokemonBaseId == 292) { return 1; } else return Mathf.FloorToInt(((pkBase.Hp * 2 + IV_Hp + BasePoints_HP / 4) * Level) / 100f) + 10 + Level; } }
    public int Attack { get { return Mathf.FloorToInt((((pkBase.Attack * 2 + IV_Attack + BasePoints_ATk / 4) * Level) / 100f + 5 + Level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int Defense { get { return Mathf.FloorToInt((((pkBase.Defence * 2 + IV_Defence + BasePoints_SPATk / 4) * Level) / 100f + 5 + Level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int SpecialAttack { get { return Mathf.FloorToInt((((pkBase.SpecialATK * 2 + IV_SpecialATK + BasePoints_Def / 4) * Level) / 100f + 5 + Level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int SpecialDefense { get { return Mathf.FloorToInt((((pkBase.SpecialDef * 2 + IV_SpecialDef + BasePoints_SPDef / 4) * Level) / 100f + 5 + Level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }
    public int Speed { get { return Mathf.FloorToInt((((pkBase.Speed * 2 + IV_Speed + BasePoints_Speed / 4) * Level) / 100f + 5 + Level) * PokemonTable.GetNatureEffect(2, natrueTypeEnum)); } }

    //技能
    private List<Move> moveCache { get; } = new List<Move>();//宝可梦所有可学技能，包含子代技能池
    private List<Move> moveForgetedCache { get; set; } = new List<Move>();//宝可梦所有可学技能，包含子代技能池
    private List<Move> curMoveCache { get; set; } = new List<Move>();
    public int Level { get => level; set => level = value; }
    public SexTypeEnum SexTypeEnum { get => sexTypeEnum; set => sexTypeEnum = value; }

    public ShinyTypeEnum ShinyType { get => shinyType; set => shinyType = value; }
    public StatusTypeEnum StatusTypeEnum { get => statusTypeEnum; set => statusTypeEnum = value; }
    public long PokemonId { get => pokemonId; }
    public int CriticalHit
    {
        get
        {
            int[] hitLevel = new int[] { 24, 8, 2, 1 };
            return hitLevel[criticalHit];
        }
        set => criticalHit = value;
    }

    private int maxMoveSize = 4;
    public List<Move> GetCurMove()
    {
        if (curMoveCache == null || curMoveCache.Capacity == 0)
        {
            this.InitCurMoveCache();
        }
        else
        {
            Debug.Log("已存在技能池");
        }
        return curMoveCache;
    }
    //生成野生pokemon的技能组
    private void InitCurMoveCache()
    {
        if (pkBase != null && pkBase.AllLearnableMoves != null)
        {
            foreach (var learnableMove in pkBase.AllLearnableMoves)
            {
                var moveBase = learnableMove.LearnableMoveBase;
                if (moveBase == null)
                {
                    Debug.LogError("moveBase is null pkbase.id:" + pkBase.PokemonBaseId);
                    continue;
                }
                Move move = new Move(moveBase, moveBase.InitialPP);
                if (learnableMove.MoveLearnedTypeEnum == MoveLearnedTypeEnum.None)
                {
                    WildPokemonAddMove(move);
                }
                else if (learnableMove.MoveLearnedTypeEnum == MoveLearnedTypeEnum.Level)
                {
                    if (Level >= learnableMove.Level)
                    {
                        WildPokemonAddMove(move);
                    }
                }
            }
        }
    }

    public void WildPokemonAddMove(Move move)
    {
        if (move == null)
        {
            Debug.LogError("WildPokemonAddMove move is null");
            return;
        }
        if (curMoveCache == null)
        {
            curMoveCache = new List<Move>
            {
                move
            };
            return;
        }
        if (curMoveCache.Count >= maxMoveSize)
        {
            ForgetMove(curMoveCache[0]);
            LearnedMove(move);
        }
        else
        {
            curMoveCache.Add(move);
        }
    }

    public void LearnedMove(Move move)
    {
        if (move != null && !moveCache.Contains(move))
        {
            if (curMoveCache == null)
                curMoveCache = new List<Move>();
            curMoveCache.Add(move);
        }
        else
            Debug.Log("学习技能失败");
    }

    public void ForgetMove(Move move)
    {
        if (move != null && curMoveCache.Contains(move))
        {
            curMoveCache.Remove(move);
            if (moveForgetedCache.Contains(move) == false)
            {
                Debug.Log("添加至Pokemon——moveForgetedCache技能池");
                moveForgetedCache.Add(move);
            }
        }
        else
            Debug.Log("遗忘技能失败");
    }

    public int GetNextLevelExp()
    {
        int num = 0;
        if (pkBase != null)
        {
            num = CommonUtils.GetExpByLevel(Level + 1, pkBase.ExpType) - CommonUtils.GetExpByLevel(Level + 1, pkBase.ExpType);
        }
        else { Debug.LogError("pkBase is null"); }
        return num;
    }

    public float GetCurExpRatio()
    {
        int nextLevelTotalExp = CommonUtils.GetExpByLevel(Level + 1, pkBase.ExpType);
        int expDetal = CommonUtils.GetExpByLevel(Level + 1, pkBase.ExpType) - CurExp;
        int curLevelExp = GetNextLevelExp();
        if (curLevelExp > 0)
        {
            return expDetal / curLevelExp;
        }
        else
        {
            if (Level >= 100)
                return 1;
            else
                return 0;
        }
    }
    public Move WildSelectMove()
    {
        var moveList = GetCurMove();
        if (moveList != null & moveList.Count != 0)
        {
            var index = Random.Range(0, -1);
            return moveList[index];
        }
        else
        {
            return null;
        }
    }
    public MoveResultEnum TakeDamage(Pokemon source, Move move)
    {
        float modifiers = Random.Range(0.85f, 1f);
        bool bIsCriticalHit = false;
        bIsCriticalHit = Mathf.FloorToInt(Random.Range(1, source.CriticalHit)) == 1;
        var attck = 0;
        var defense = 0;
        switch (move.MoveBase.MoveTypeEnum)
        {
            case MoveTypeEnum.Special_Move:
                attck = source.SpecialAttack;
                defense = SpecialDefense;
                break;
            case MoveTypeEnum.Physical_Move:
                attck = source.Attack;
                defense = Defense;
                break;
        }
        float typeEffect = PokemonTable.GetTypeEffect((int)move.MoveBase.MoveType, pkBase.PokemonType1);
        float typeEffect2 = PokemonTable.GetTypeEffect((int)move.MoveBase.MoveType, pkBase.PokemonType2);
        float sourceTypeEffect = (move.MoveBase.MoveType == source.pkBase.PokemonType1 || move.MoveBase.MoveType == source.pkBase.PokemonType2) ? 1.5f : 1f;
        float hitRate = bIsCriticalHit ? 1.5f : 1f;
        float damage = ((2f * source.Level + 10f) / 250f * ((float)attck / defense) * move.MoveBase.Power) * typeEffect * typeEffect2 * sourceTypeEffect * hitRate * modifiers;
        if (damage > 0) damage = Mathf.Max(damage, 1);
        damage=Mathf.FloorToInt(damage);
        Debug.Log("伤害" + damage);
        CurHp -= Mathf.FloorToInt(damage);
        if (damage == 0)
        {
            return MoveResultEnum.NotEffective;
        }
        if (sourceTypeEffect > 1f)
        {
            if (bIsCriticalHit)
            {
                return MoveResultEnum.SuperEffective_CriticalHit;
            }
            else
            {
                return MoveResultEnum.SuperEffective;
            }
        }
        else if (sourceTypeEffect < 1f && sourceTypeEffect > 0)
        {
            if (bIsCriticalHit)
            {
                return MoveResultEnum.NotVeryEffective_CriticalHit;
            }
            else
            {
                return MoveResultEnum.NotEffective;
            }
        }
        else
        {
            if (bIsCriticalHit)
            {
                return MoveResultEnum.Effective_CriticalHit;
            }
            else
            {
                return MoveResultEnum.Effective;
            }
        }
    }

    /*初始化信息（一些没有在构造函数处理的）*/
    public void Init()
    {
        InitCurHp();
    }
    /*初始化血量*/
    public void InitCurHp()
    {
        CurHp = MaxHp;
    }
    /*随机个体*/
    public void RandomIV()
    {
        IV_Hp = Random.Range(1, 31);
        IV_Attack = Random.Range(1, 31);
        IV_Defence = Random.Range(1, 31);
        IV_SpecialATK = Random.Range(1, 31);
        IV_SpecialDef = Random.Range(1, 31);
        IV_Speed = Random.Range(1, 31);
    }
    //全参构造函数
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, int catchArea, CatchTypeEnum catchTypeEnum, string nickName, int basePoints_HP, int basePoints_ATk, int basePoints_SPATk, int basePoints_Def, int basePoints_SPDef, int basePoints_Speed, int basePoints_Friendship, int basePoints_Beauty, int basePoints_Cute, int basePoints_Smart, int basePoints_Cool, int basePoints_Tough, int iV_Hp, int iV_Attack, int iV_Defence, int iV_SpecialATK, int iV_SpecialDef, int iV_Speed, int curHp, List<Move> moveCache, List<Move> moveForgetedCache, List<Move> curMoveCache, int maxMoveSize, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.Level = level;
        pokemonId = id;
        this.ownerId = ownerId;
        this.catchArea = catchArea;
        this.catchTypeEnum = catchTypeEnum;
        this.nickName = nickName ?? throw new System.ArgumentNullException(nameof(nickName));
        BasePoints_HP = basePoints_HP;
        BasePoints_ATk = basePoints_ATk;
        BasePoints_SPATk = basePoints_SPATk;
        BasePoints_Def = basePoints_Def;
        BasePoints_SPDef = basePoints_SPDef;
        BasePoints_Speed = basePoints_Speed;
        BasePoints_Friendship = basePoints_Friendship;
        BasePoints_Beauty = basePoints_Beauty;
        BasePoints_Cute = basePoints_Cute;
        BasePoints_Smart = basePoints_Smart;
        BasePoints_Cool = basePoints_Cool;
        BasePoints_Tough = basePoints_Tough;
        IV_Hp = iV_Hp;
        IV_Attack = iV_Attack;
        IV_Defence = iV_Defence;
        IV_SpecialATK = iV_SpecialATK;
        IV_SpecialDef = iV_SpecialDef;
        IV_Speed = iV_Speed;
        CurHp = curHp;
        this.moveCache = moveCache ?? throw new System.ArgumentNullException(nameof(moveCache));
        this.moveForgetedCache = moveForgetedCache ?? throw new System.ArgumentNullException(nameof(moveForgetedCache));
        this.curMoveCache = curMoveCache ?? throw new System.ArgumentNullException(nameof(curMoveCache));
        this.maxMoveSize = maxMoveSize;
        this.natrueTypeEnum = natrueTypeEnum;
    }

    //省略技能池
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, int catchArea, CatchTypeEnum catchTypeEnum, string nickName, int basePoints_HP, int basePoints_ATk, int basePoints_SPATk, int basePoints_Def, int basePoints_SPDef, int basePoints_Speed, int basePoints_Friendship, int basePoints_Beauty, int basePoints_Cute, int basePoints_Smart, int basePoints_Cool, int basePoints_Tough, int iV_Hp, int iV_Attack, int iV_Defence, int iV_SpecialATK, int iV_SpecialDef, int iV_Speed, int curHp, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.Level = level;
        pokemonId = id;
        this.ownerId = ownerId;
        this.catchArea = catchArea;
        this.catchTypeEnum = catchTypeEnum;
        this.nickName = nickName ?? throw new System.ArgumentNullException(nameof(nickName));
        BasePoints_HP = basePoints_HP;
        BasePoints_ATk = basePoints_ATk;
        BasePoints_SPATk = basePoints_SPATk;
        BasePoints_Def = basePoints_Def;
        BasePoints_SPDef = basePoints_SPDef;
        BasePoints_Speed = basePoints_Speed;
        BasePoints_Friendship = basePoints_Friendship;
        BasePoints_Beauty = basePoints_Beauty;
        BasePoints_Cute = basePoints_Cute;
        BasePoints_Smart = basePoints_Smart;
        BasePoints_Cool = basePoints_Cool;
        BasePoints_Tough = basePoints_Tough;
        IV_Hp = iV_Hp;
        IV_Attack = iV_Attack;
        IV_Defence = iV_Defence;
        IV_SpecialATK = iV_SpecialATK;
        IV_SpecialDef = iV_SpecialDef;
        IV_Speed = iV_Speed;
        CurHp = curHp;
        this.natrueTypeEnum = natrueTypeEnum;
    }

    //生成野生的构造函数（不带技能池）
    public Pokemon(PokemonBase pkBase, int level, long id, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.Level = level;
        pokemonId = id;
        CurHp = MaxHp;
        this.natrueTypeEnum = natrueTypeEnum;
    }
    public Pokemon(PokemonBase pkBase, int level, long id, long ownerId, NatrueTypeEnum natrueTypeEnum)
    {
        this.pkBase = pkBase ?? throw new System.ArgumentNullException(nameof(pkBase));
        this.Level = level;
        pokemonId = id;
        this.ownerId = ownerId;
        CurHp = MaxHp;
        this.natrueTypeEnum = natrueTypeEnum;
    }
}
