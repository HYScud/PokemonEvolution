using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase pokemonUnitBase;
    [SerializeField] int level;
    [SerializeField] Pokemon pokemonUnit;
    [SerializeField] Image PokemonImage;
    [SerializeField] bool bIsPlayer;

    public Pokemon PokemonUnit { get => pokemonUnit; set => pokemonUnit = value; }

    public void Init()
    {
        PokemonUnit = new Pokemon(pokemonUnitBase, level, Random.Range(0, 10000000), NatrueTypeEnum.Hardy);
        string path = "UI/PokemonSprite/";
        if (bIsPlayer)
        {
            if (PokemonUnit.ShinyType == ShinyTypeEnum.None)
            {
                path += "Back/" + PokemonUnit.pkBase.PokemonBaseId;
            }
            else
            {
                path += "Back_Shiny/" + PokemonUnit.pkBase.PokemonBaseId;
            }
        }
        else
        {
            if (PokemonUnit.ShinyType == ShinyTypeEnum.None)
            {
                path += "Front/" + PokemonUnit.pkBase.PokemonBaseId;
            }
            else
            {
                path += "Front_Shiny/" + PokemonUnit.pkBase.PokemonBaseId;
            }
        }
        SetSprite(path);
    }
    public void Init(Pokemon pokemon)
    {
        //pokemonUnit = pokemon;
        string path = "UI/PokemonSprite/Back/";
        if (bIsPlayer)
        {
            if (pokemon.ShinyType == ShinyTypeEnum.None)
            {
                path += "Back/" + pokemon.pkBase.PokemonBaseId + "_0";
            }
            else
            {
                path += "Back_Shiny/" + pokemon.pkBase.PokemonBaseId + "_0";
            }
        }
        else
        {
            if (pokemon.ShinyType == ShinyTypeEnum.None)
            {
                path += "Front/" + pokemon.pkBase.PokemonBaseId + "_0";
            }
            else
            {
                path += "Front_Shiny/" + pokemon.pkBase.PokemonBaseId + "_0";
            }
        }
        SetSprite(path);
    }

    public void SetSprite(string path)
    {
        if (PokemonImage != null)
        {
            PokemonImage.sprite = Resources.Load<Sprite>(path);
        }
    }
    void Awake()
    {
        PokemonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
