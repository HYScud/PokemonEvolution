public class Move
{
    public Move(MoveBase moveBase, int curPP)
    {
        this.moveBase = moveBase;
        this.curPP = curPP;
    }

    private MoveBase moveBase { get; set; }

    private int curPP { get; set; }

    public override bool Equals(object obj)
    {
        Move target = obj as Move;
        if (target == null || target.moveBase == null || moveBase == null)
            return false;
        else
            return moveBase.MoveId == target.moveBase.MoveId;
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
