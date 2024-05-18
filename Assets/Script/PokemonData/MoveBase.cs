using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "CreateDataBase/Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] int moveId;
    [SerializeField] string moveName;
    [SerializeField] int maxPP;
    [SerializeField] int initialPP;
    [SerializeField] TypeEnum moveType;
    [TextArea]
    [SerializeField] string moveDesc;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int priority;
    [SerializeField] MoveATKRangeEnum moveATKRangeEnum = MoveATKRangeEnum.Single;//攻击个数
    [SerializeField] MoveContactEnum moveContactEnum = MoveContactEnum.None;//接触类型
    [SerializeField] MoveTypeEnum moveTypeEnum = MoveTypeEnum.Physical_Move;//技能类型
    [SerializeField] bool bIsEffectByProtect = false;//是否受守住影响
    [SerializeField] bool bIsEffectByMagicCoat = false;//是否受魔法反射影响
    [SerializeField] bool bIsEffectBySnatch = false;//是否可以被抢夺
    [SerializeField] bool bIsEffectByMirror = false;//是否受鹦鹉学舌影响
    [SerializeField] bool bIsEffectByItem = false;//是否受王者之证等道具影响

    public int MoveId { get => moveId; }
    public string MoveName { get => moveName; }
    public int MaxPP { get => maxPP; }
    public int InitialPP { get => initialPP; }
    public TypeEnum MoveType { get => moveType; }
    public string MoveDesc { get => moveDesc; }
    public int Power { get => power; }
    public int Accuracy { get => accuracy; }
    public int Priority { get => priority; }
    public MoveATKRangeEnum MoveATKRangeEnum { get => moveATKRangeEnum; }
    public MoveContactEnum MoveContactEnum { get => moveContactEnum; }
    public MoveTypeEnum MoveTypeEnum { get => moveTypeEnum; }
    public bool BIsEffectByProtect { get => bIsEffectByProtect; }
    public bool BIsEffectByMagicCoat { get => bIsEffectByMagicCoat; }
    public bool BIsEffectBySnatch { get => bIsEffectBySnatch; }
    public bool BIsEffectByMirror { get => bIsEffectByMirror; }
    public bool BIsEffectByItem { get => bIsEffectByItem; }
}
