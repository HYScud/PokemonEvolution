using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    /*game字段*/
    private Vector2 m_Input;//输入接受
    public LayerMask BuildingLayer;//建筑层
    public LayerMask GrassLayer;//浅草层
    public LayerMask DeepGrassLayer;
    private PlayerAnimator playerAnimator;//玩家动画控制
    private bool isMoving = false;//是否可以移动

    /*玩家数据*/
    public float moveSpeed = 6;//玩家移动速度
    private Vector3 vector3 = new Vector3(0, 0, 0);//玩家坐标
    private MoveDirection playerCurDir = MoveDirection.Down;//玩家朝向
    private PlayerStateEnum playerState = PlayerStateEnum.Normal;//玩家状态
    private PlayerDataStore playerInfo;//玩家信息（TODO：后续把上面的玩家信息包装到playerInfo）

    /*构造器*/
    public MoveDirection PlayerCurDir { get => playerCurDir; set => playerCurDir = value; }
    internal PlayerStateEnum PlayerState { get => playerState; }

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerInfo = new PlayerDataStore(1,"luyisac");
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = vector3;
        this.SetOrder();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            this.ToMove();
        }
        else
        {

        }
    }

    private void ToMove()
    {
        if (!isMoving && PlayerState == PlayerStateEnum.Normal)
        {
            m_Input.x = Input.GetAxisRaw("Horizontal");
            m_Input.y = Input.GetAxisRaw("Vertical");
            if (m_Input != Vector2.zero)
            {
                Debug.Log("执行ToMove" + m_Input.x + " " + m_Input.y);
                if (m_Input.x != 0)
                {
                    m_Input.y = 0;
                }
                playerAnimator.ChangeMoveX(m_Input.x);
                playerAnimator.ChangeMoveY(m_Input.y);
                var targetPos = transform.position;
                targetPos.x += m_Input.x;
                targetPos.y += m_Input.y;
                if (m_Input.Equals(Vector2.zero))
                {
                    return;
                }
                playerState = PlayerStateEnum.Busy;
                if (CheckSameDirWithNextMove(m_Input))
                {
                    isMoving = true;
                    if (CheckWalkable(targetPos) && CommonUtils.CheckIsReachable(targetPos))
                        Move(targetPos).Forget();
                    else
                    /*TODO:不能移动也能播放走路动画*/
                    {
                        playerState = PlayerStateEnum.Normal;
                        isMoving = false;
                    }
                }
                else
                {
                    Debug.Log("change face dir");
                    ChangeFaceDir(m_Input).Forget();
                }
            }
            else
            {
                isMoving = false;
            }
        }
        playerAnimator.ChangeisMoving(isMoving);
    }

    public async UniTaskVoid ChangeFaceDir(Vector2 pos)
    {
        if (pos.x == 1)
        {
            playerCurDir = MoveDirection.Right;
            playerAnimator.SetRoleFaceDir(MoveDirection.Right);
        }
        if (pos.x == -1)
        {
            playerCurDir = MoveDirection.Left;
            playerAnimator.SetRoleFaceDir(MoveDirection.Left);
        }
        if (pos.y == -1)
        {
            playerCurDir = MoveDirection.Down;
            playerAnimator.SetRoleFaceDir(MoveDirection.Down);
        }
        if (pos.y == 1)
        {
            playerCurDir = MoveDirection.Up;
            playerAnimator.SetRoleFaceDir(MoveDirection.Up);
        }
        await UniTask.DelayFrame(50);
        isMoving = false;
        playerState = PlayerStateEnum.Normal;
    }

    public bool CheckSameDirWithNextMove(Vector2 pos)
    {
        Debug.Log("******Check Same" + pos + "******CurDir" + playerCurDir);
        if (pos.x == 1 && playerCurDir == MoveDirection.Right)
        {
            return true;
        }
        if (pos.x == -1 && playerCurDir == MoveDirection.Left)
        {
            return true;
        }
        if (pos.y == 1 && playerCurDir == MoveDirection.Up)
        {
            return true;
        }
        if (pos.y == -1 && playerCurDir == MoveDirection.Down)
        {
            return true;
        }
        return false;
    }
    async UniTaskVoid Move(Vector3 targetPos)
    {
        Debug.Log("moveTargetPos：" + targetPos);
        CommonUtils.SaveCharacterPos(playerInfo.PlayerId, targetPos);
        while ((targetPos - transform.position).sqrMagnitude > 0.00001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            await UniTask.Yield();
            await UniTask.NextFrame();
        }
        Debug.Log("Player MoveOver curPos" + transform.position + "**" + targetPos);
        transform.position = targetPos;
        CommonUtils.CorrectPosition(transform.position);
        isMoving = false;
        playerState = PlayerStateEnum.Normal;
        this.SetOrder();
        CheckForEncounters();
    }

    public bool CheckWalkable(Vector3 targetPos)
    {
        //这里处理并不是很好，会有房子Tile的collider，目前无法手动设置collider范围 出现半格（TODO：优化检测/替换collider）
        if (Physics2D.OverlapCircle(targetPos, 0.15f, BuildingLayer) != null)
        {
            return false;
        }
        return true;
    }

    public void CheckForEncounters()
    {
        Debug.Log("打印检测进入战斗");
        //这里处理并不是很好，会有房子Tile的collider，目前无法手动设置collider范围 出现半格（TODO：优化检测/替换collider）
        if (Physics2D.OverlapCircle(transform.position, 0.15f, GrassLayer) != null)
        {
            if (Random.Range(1, 200) > 30)
            {
                if (Random.Range(1, 10) > 7)
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Multiple);
                else
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Single);
            }
        }
        if (Physics2D.OverlapCircle(transform.position, 0.15f, DeepGrassLayer) != null)
        {
            if (Random.Range(1, 200) > 30)
            {
                var battleTypeRandom = Random.Range(1, 10);
                if (battleTypeRandom > 8)
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Multiple);
                else if (battleTypeRandom > 4)
                {
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Double);
                }
                else
                    TurnToBatlle(BattleTypeEnum.Wild, BattleNumTypeEnum.Single);
            }
        }
    }

    public void TurnToBatlle(BattleTypeEnum battleTypeEnum, BattleNumTypeEnum battleNumTypeEnum)
    {
        Debug.Log("Player Enter Battle. BattleType" + battleTypeEnum);
        playerState = PlayerStateEnum.Battle;
    }

    public void SetOrder()
    {
        var spriteRender = gameObject.GetComponent<SpriteRenderer>();
        spriteRender.sortingOrder = Mathf.FloorToInt(transform.position.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }

}
