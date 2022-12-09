using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data
{
    public float positionX;    
    public float positionY;
    public float positionZ;
    public string Pokemon;
    public string Ability1;
    public string Ability2;
    public string Ability3;
    public string Ability4;

    public Data(DataBase data)
    {
        positionX = data.savedPosition.x;
        positionY = data.savedPosition.y;
        positionZ = data.savedPosition.z;
        Pokemon = data.GetPlayerPokemon().name;
        Ability1 = data.GetPlayerPokemon().Abilities[0].name;
        Ability2 = data.GetPlayerPokemon().Abilities[1].name;
        Ability3 = data.GetPlayerPokemon().Abilities[2].name;
        Ability4 = data.GetPlayerPokemon().Abilities[3].name;
    }
}
