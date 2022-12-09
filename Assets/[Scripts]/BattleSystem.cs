using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Truen-based menu battle system.
/// Four options of Ability. Appear clickable UI buttons showing their name.
/// Win and loss condtion
/// </summary>

public class BattleSystem : Singleton<BattleSystem>
{
    [Header("Battle System Status")]
    [SerializeField] private bool isBattleEnd = false;

    [Header("Actor Propeties")]
    public Pokemon playerPokemon;
    public List<Ability> playerAbilities;
    public Pokemon opponentPokemon;
    public List<Ability> opponentAbilities;

    [Header("Battle Properties")]
    public TextMeshProUGUI battleMessageText;
    public string battleMessage;

    [Header("System Properties")]
    public ActorStatProperty playerStatProperty;
    public ActorStatProperty opponentStatProperty;
    public Pokemon[] pokemonList;
    public List<Button> playerAbilityButtons;

    [Header("Debuggin Purpose")]
    [SerializeField] private bool isPlayerTurn = true;
    [SerializeField] private bool isOpponentTurn = false;
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Pokemon defaultPokemon;

    private void Awake()
    {
        var obj = FindObjectsOfType<BattleSystem>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Get around system properties
        playerStatProperty = GameObject.Find("PlayerPokemon_Back").GetComponent<ActorStatProperty>();
        opponentStatProperty = GameObject.Find("OpponentPokemon_Front").GetComponent<ActorStatProperty>();
        if (playerStatProperty == null || opponentStatProperty == null) { Debug.LogError("Actor properties are null"); return; }

        SetPlayer();
        SetOpponent();

        if (playerAbilities == null || opponentAbilities == null) { Debug.LogError("Actor abilities are null"); return; }

    }

    private void SetPlayer()
    {
        // Get the player's pokemon from database
        playerPokemon = Singleton<DataBase>.Instance.GetPlayerPokemon() == null ? defaultPokemon : Singleton<DataBase>.Instance.GetPlayerPokemon();
        if (playerPokemon == null) return;

        // Get Pokemon ability list
        playerAbilities = playerPokemon.abilities;

        // Apply player Pokemon abilities to action buttons
        for (int i = 0; i < playerAbilities.Count; i++)
        {
            if (playerAbilities[i] != null)
            {
                playerAbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerAbilities[i].abilityName;
            }
            else
            {
                playerAbilityButtons[i].gameObject.SetActive(false);
            }
        }

        // Set Actor properties
        playerStatProperty.SetPokemonSprite = playerPokemon.BackSprite;
        playerStatProperty.CurrentHealth = playerPokemon.CurrentHp;
        playerStatProperty.MaxHealth = playerPokemon.MaxHp;
    }

    private void SetOpponent()
    {
        pokemonList = Resources.LoadAll<Pokemon>("Pokemon");

        // Generate an opponent's pokemon
        opponentPokemon = pokemonList[Random.Range(0, pokemonList.Length)];
        if (opponentPokemon == null) return;

        // Get Pokemon ability list
        opponentAbilities = opponentPokemon.abilities;

        // Set Actor properties
        opponentStatProperty.SetPokemonSprite = opponentPokemon.FrontSprite;
        opponentStatProperty.CurrentHealth = opponentPokemon.CurrentHp;
        opponentStatProperty.MaxHealth = opponentPokemon.MaxHp;
    }

    private void Update()
    {
        if (!isPlayerTurn && !isBattleEnd)
        {
            StartCoroutine(UseOpponentAbility());
            isOpponentTurn = true;
            ToggleActorTurn();
        }
        else if ((isPlayerTurn && !isOpponentTurn) || isBattleEnd)
        {
            StopCoroutine(UseOpponentAbility());
        }
    }

    IEnumerator UseOpponentAbility()
    {
        yield return new WaitForSeconds(2.0f);

        //int randNum = Random.Range(0, opponentAbilities.Count);        
        //opponentAbilities[randNum].UseAbility(opponentPokemon, playerPokemon);

        OpponentSelectAbility().UseAbility(opponentPokemon, playerPokemon);

        isOpponentTurn = false;

        if (isDebugging) Debug.Log($"End opponent turn. _isOpponentTurn = {isOpponentTurn}");
        yield return null;
    }

    private Ability OpponentSelectAbility()
    {
        // 0 (33%): choose random ability
        // 1 and 2 (66%) : choose ability according to condition
        if (Random.Range(0, 3) >= 1)
        {
            Debug.Log($"Opponent chose to using a brain!");

            if (opponentPokemon.CurrentHp <= opponentPokemon.MaxHp / 10)
            {
                for (int index = 0; index < opponentAbilities.Count; index++)
                {
                    if (opponentAbilities[index].abilityType == AbilityType.FLEE)
                    {
                        Debug.Log($"Opponent chose a FLEE ability.");
                        return opponentAbilities[index];
                    }
                }
            }
            else if (opponentPokemon.CurrentHp <= opponentPokemon.MaxHp / 2)
            {
                for (int index = 0; index < opponentAbilities.Count; index++)
                {
                    if (opponentAbilities[index].abilityType == AbilityType.HEAL)
                    {
                        Debug.Log($"Opponent chose a HEAL ability.");
                        return opponentAbilities[index];
                    }
                }
            }
            else if (playerPokemon.CurrentHp >= playerPokemon.MaxHp / 2)
            {
                for (int index = 0; index < opponentAbilities.Count; index++)
                {
                    if (opponentAbilities[index].abilityType == AbilityType.MELEEATTACK)
                    {
                        Debug.Log($"Opponent chose a MELEEATTACK ability.");
                        return opponentAbilities[index];
                    }
                }
            }
        }

        // 0 (33%): choose random ability
        // if opponent doesn't have a matched ability
        Debug.Log($"Opponent chose a random ability.");
        return opponentAbilities[Random.Range(0, opponentAbilities.Count)];
    }

    private void ToggleActorTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (isDebugging) Debug.Log($"_isPlayerTurn = {isPlayerTurn}");
    }

    public void EndBattle()
    {
        isBattleEnd = true;

        if (isDebugging) Debug.Log($"Battle End. _isInBattle = {isBattleEnd}");
    }



    #region Ability Button Functions
    public void OnClickAbility1Button()
    {
        if (isPlayerTurn && !isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[0] != null)
            {
                playerAbilities[0].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickAbility2Button()
    {
        if (isPlayerTurn && !isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[1] != null)
            {
                playerAbilities[1].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickAbility3Button()
    {
        if (isPlayerTurn && !isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[2] != null)
            {
                playerAbilities[2].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickAbility4Button()
    {
        if (isPlayerTurn && !isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[3] != null)
            {
                playerAbilities[3].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    #endregion
}
