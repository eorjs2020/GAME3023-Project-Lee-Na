using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : Singleton<DataBase>
{
    public Vector3 savedPosition;
    public Vector3 originalPosition;
    public bool isThereSave;
    public Pokemon pokemon;

    private void Awake()
    {
        var obj = FindObjectsOfType<DataBase>();
        if (obj.Length == 1)
        {
            LoadGame();
            DontDestroyOnLoad(gameObject);             
        }
        else
        {
            Destroy(gameObject);
        }
    }   

    public Pokemon GetPlayerPokemon()
    {
        return playerPokemon;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveGame();
        }
    }

    public void SaveGame()
    {
        SaveLoad.SaveData(this);
        Debug.Log(Application.persistentDataPath);
    }

    public void LoadGame()
    {
        
        Data data = SaveLoad.LoadData();
        if (data != null)
        {
            Vector3 temp = GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
            temp.x = data.positionX;
            temp.y = data.positionY;
            temp.z = data.positionZ;
            pokemon = Resources.Load<Pokemon>($"Pokemon/{data.Pokemon}");
            pokemon.Abilities.Clear();
            pokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability1}"));
            pokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability2}"));
            pokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability3}"));
            pokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability4}"));
        }
    }


}