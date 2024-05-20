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
    private bool bIsPlayer;

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, ActionTypeEnum actionType, bool bIsPlayer)
    {
        this.SourcePokemon = sourcePokemon;
        this.TargetPokemon = targetPokemon;
        this.ActionType = actionType;
        this.BIsPlayer = bIsPlayer;
    }

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, Move move, ActionTypeEnum actionType, bool bIsPlayer)
    {
        this.SourcePokemon = sourcePokemon;
        this.TargetPokemon = targetPokemon;
        this.Move = move;
        this.ActionType = actionType;
        this.BIsPlayer = bIsPlayer;
    }

    public BattleAction(Pokemon sourcePokemon, Pokemon targetPokemon, Move move, int useItemID, ActionTypeEnum actionType, bool bIsPlayer)
    {
        this.SourcePokemon = sourcePokemon;
        this.TargetPokemon = targetPokemon;
        this.Move = move;
        this.useItemID = useItemID;
        this.BIsPlayer = bIsPlayer;
        this.ActionType = actionType;
    }

    public Pokemon SourcePokemon { get => sourcePokemon; private set => sourcePokemon = value; }
    public Move Move { get => move; private set => move = value; }
    public ActionTypeEnum ActionType { get => actionType; set => actionType = value; }
    public Pokemon TargetPokemon { get => targetPokemon; set => targetPokemon = value; }
    public bool BIsPlayer { get => bIsPlayer; set => bIsPlayer = value; }

    public override bool Equals(object obj)
    {
        var other = obj as BattleAction;
        if (other == null) return false;
        return this.SourcePokemon.PokemonId.Equals(other.SourcePokemon.PokemonId);
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
