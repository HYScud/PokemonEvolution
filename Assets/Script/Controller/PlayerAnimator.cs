using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerAnimator : MonoBehaviour
{

    private PlayerActionStateEnum curState = PlayerActionStateEnum.Normal;
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] List<Sprite> walkRightSprites;

    [SerializeField] List<Sprite> runDownSprites;
    [SerializeField] List<Sprite> runUpSprites;
    [SerializeField] List<Sprite> runLeftSprites;
    [SerializeField] List<Sprite> runRightSprites;

    [SerializeField] List<Sprite> BicycleDownSprites;
    [SerializeField] List<Sprite> BicycleUpSprites;
    [SerializeField] List<Sprite> BicycleLeftSprites;
    [SerializeField] List<Sprite> BicycleRightSprites;

    [SerializeField] List<Sprite> WaterWalkDownSprites;
    [SerializeField] List<Sprite> WaterWalkUpSprites;
    [SerializeField] List<Sprite> WaterWalkLeftSprites;
    [SerializeField] List<Sprite> WaterWalkRightSprites;

    private CharacterAnimator animator;
    internal PlayerActionStateEnum CurState { get => curState; set => curState = value; }

    public void ChangeMoveX(float moveX)
    {
        animator.MoveX = moveX;
    }

    public void ChangeMoveY(float moveY)
    {
        animator.MoveY = moveY;
    }
    public void ChangeisMoving(bool isMoving)
    {
        animator.IsMoving = isMoving;
    }

    // Start is called before the first frame update
    public void Awake()
    {
        Debug.Log("PlayerAnimator Awake");
        animator = GetComponent<CharacterAnimator>();
        this.SetCurrentSpritesGroup();
    }

    private void SetCurrentSpritesGroup()
    {
        Debug.Log("…Ë÷√∂Øª≠Õº∆¨◊È");
        switch (curState)
        {
            case PlayerActionStateEnum.Run:
                animator.DownSprites = runDownSprites;
                animator.UpSprites = runDownSprites;
                animator.LeftSprites = runDownSprites;
                animator.RightSprites = runDownSprites;
                break;
            case PlayerActionStateEnum.Bicycle:
                animator.DownSprites = BicycleDownSprites;
                animator.UpSprites = BicycleUpSprites;
                animator.LeftSprites = BicycleLeftSprites;
                animator.RightSprites = BicycleRightSprites;
                break;
            case PlayerActionStateEnum.WaterWalk:
                animator.DownSprites = WaterWalkDownSprites;
                animator.UpSprites = WaterWalkUpSprites;
                animator.LeftSprites = WaterWalkLeftSprites;
                animator.RightSprites = WaterWalkRightSprites;
                break;
            default:
                animator.DownSprites = walkDownSprites;
                animator.UpSprites = walkUpSprites;
                animator.LeftSprites = walkLeftSprites;
                animator.RightSprites = walkRightSprites;
                break;
        }
    }

    public void SetRoleFaceDir(MoveDirection direction)
    {

    }
}
