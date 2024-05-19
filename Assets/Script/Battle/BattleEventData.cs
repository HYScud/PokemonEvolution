using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEventData : EventData
{
    private Move selectedMove;

    public BattleEventData(Move move,EventID eid) : base(eid)
    {
        this.SelectedMove = move;
    }
    public Move SelectedMove { get => selectedMove; private set => selectedMove = value; }
}
