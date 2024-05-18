using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDataStore
{
    private long playerId;
    private string playerName;

    private List<Pokemon> playerPokemon = new List<Pokemon>();

    public long PlayerId { get => playerId; }
    public string PlayerName { get => playerName; }
    public List<Pokemon> PlayerPokemon { get => playerPokemon; set => playerPokemon = value; }

    public PlayerDataStore(long playerId, string playerName, List<Pokemon> playerPokemon)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        PlayerPokemon = playerPokemon;
        Init();
    }

    public PlayerDataStore(long playerId, string playerName)
    {
        this.playerId = playerId;
        this.playerName = playerName;
        PlayerPokemon = new List<Pokemon>();
        Init();
    }

    public void Init()
    {
        PokemonBase pokemonBase = ScriptableObject.CreateInstance<PokemonBase>();
        int level = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 30f));
        Pokemon tempPokemon = new Pokemon(pokemonBase, level, Mathf.RoundToInt(UnityEngine.Random.Range(0f, 3000f)), NatrueTypeEnum.Mild);
        playerPokemon.Add(tempPokemon);
    }
}
