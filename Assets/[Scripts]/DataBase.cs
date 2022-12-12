using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DataBase : Singleton<DataBase>
{
    public Vector3 savedPosition;
    public Vector3 quickSavedPosition;    
    public Pokemon playerPokemon;

    private Vector3 originalPosition = new Vector3(-16.518f, -12.5f, 0);
    public bool isLoose = true;    
    private void Awake()
    {
        //Duplication Check
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

    public void SaveGame()
    {
        //Saving Function
        savedPosition = GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position;
        SaveLoad.SaveData(this);
        Debug.Log(Application.persistentDataPath);
    }   

    public void LoadGame()
    {        
        Data data = SaveLoad.LoadData();
        if (data != null)
        {
            savedPosition = new Vector3( data.positionX, data.positionY, data.positionZ);            
            playerPokemon = Resources.Load<Pokemon>($"Pokemon/{data.Pokemon}");
            playerPokemon.Abilities.Clear();
            playerPokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability1}"));
            playerPokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability2}"));
            playerPokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability3}"));
            playerPokemon.Abilities.Add(Resources.Load<Ability>($"Abilities/{data.Ability4}"));            
        }
        else
        {
            NewGame();
        }
    }

    public void LoadPlayer()
    {
        GameObject.FindObjectOfType<PlayerController>().gameObject.transform.position = savedPosition;
    }

    public void LoseBattle()
    {
        isLoose = true;
        SceneManager.LoadScene("MainScene");       
    }

    public void WinBattle()
    {
        isLoose = false;
        SceneManager.LoadScene("MainScene");
    }

    public void NewGame()
    {
        savedPosition = originalPosition;
        playerPokemon = Resources.Load<Pokemon>("Pokemon/Charmander");
    }


   


}