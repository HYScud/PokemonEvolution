public class Move
{
    public Move(MoveBase moveBase, int curPP)
    {
        this.MoveBase = moveBase;
        this.CurPP = curPP;
    }

    private MoveBase moveBase;

    private int curPP;
    public MoveBase MoveBase { get => moveBase; set => moveBase = value; }
    public int CurPP { get => curPP; set => curPP = value; }

    public override bool Equals(object obj)
    {
        Move target = obj as Move;
        if (target == null || target.MoveBase == null || MoveBase == null)
            return false;
        else
            return MoveBase.MoveId == target.MoveBase.MoveId;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
