using UnityEngine;

[System.Serializable]
public class LearnableMove
{
    [SerializeField] MoveBase learnableMoveBase;
    [SerializeField] int level;
    [SerializeField] MoveLearnedTypeEnum moveLearnedTypeEnum = MoveLearnedTypeEnum.None;

    public MoveBase LearnableMoveBase { get => learnableMoveBase; }
    public int Level { get => level; }
    public MoveLearnedTypeEnum MoveLearnedTypeEnum { get => moveLearnedTypeEnum; }
}
