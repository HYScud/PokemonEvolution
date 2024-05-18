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
        PokemonUnit = new Pokemon(pokemonUnitBase, level, Random.Range(0, 100), NatrueTypeEnum.Hardy);
        string path = "UI/PokemonSprite/";
        if (bIsPlayer)
        {
            if (PokemonUnit.ShinyType == ShinyTypeEnum.None)
            {
                path += "Back/" + PokemonUnit.pkBase.PokemonId;
            }
            else
            {
                path += "Back_Shiny/" + PokemonUnit.pkBase.PokemonId;
            }
        }
        else
        {
            if (PokemonUnit.ShinyType == ShinyTypeEnum.None)
            {
                path += "Front/" + PokemonUnit.pkBase.PokemonId;
            }
            else
            {
                path += "Front_Shiny/" + PokemonUnit.pkBase.PokemonId;
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
                path += "Back/" + pokemon.pkBase.PokemonId + "_0";
            }
            else
            {
                path += "Back_Shiny/" + pokemon.pkBase.PokemonId + "_0";
            }
        }
        else
        {
            if (pokemon.ShinyType == ShinyTypeEnum.None)
            {
                path += "Front/" + pokemon.pkBase.PokemonId + "_0";
            }
            else
            {
                path += "Front_Shiny/" + pokemon.pkBase.PokemonId + "_0";
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
    void Start()
    {
        PokemonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
