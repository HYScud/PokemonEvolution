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
        this.SourcePokemon = sourcePokemon;
        this.targetPokemon = targetPokemon;
        this.actionType = actionType;
    }

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, Move move, ActionTypeEnum actionType)
    {
        this.SourcePokemon = sourcePokemon;
        this.targetPokemon = targetPokemon;
        this.Move = move;
        this.actionType = actionType;
    }

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, Move move, int useItemID, ActionTypeEnum actionType)
    {
        this.SourcePokemon = sourcePokemon;
        this.targetPokemon = targetPokemon;
        this.Move = move;
        this.useItemID = useItemID;
        this.actionType = actionType;
    }

    public Pokemon SourcePokemon { get => sourcePokemon; private set => sourcePokemon = value; }
    public Move Move { get => move; private set => move = value; }
}
