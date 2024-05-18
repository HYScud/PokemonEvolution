using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction
{
    private Pokemon sourcePokemon;
    private Pokemon targetPokemon;
    private Move move;
    private int useItemID;
    private ActionTypeEnum actionType;

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, ActionTypeEnum actionType)
    {
        this.sourcePokemon = sourcePokemon;
        this.targetPokemon = targetPokemon;
        this.actionType = actionType;
    }

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, Move move, int useItemID, ActionTypeEnum actionType)
    {
        this.sourcePokemon = sourcePokemon;
        this.targetPokemon = targetPokemon;
        this.move = move;
        this.useItemID = useItemID;
        this.actionType = actionType;
    }

}
