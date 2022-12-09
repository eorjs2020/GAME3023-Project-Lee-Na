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
    public List<Button> playerAbilityButtons;

    [Header("Debuggin Purpose")]
    [SerializeField] private bool isPlayerTurn = true;
    [SerializeField] private bool isOpponentTurn = false;
    [SerializeField] private bool isDebugging = false;

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
        // Get each actor pokemon
        playerPokemon = GameObject.Find("PlayerPokemon").GetComponent<ActorStatProperty>().pokemon;
        opponentPokemon = GameObject.Find("OpponentPokemon").GetComponent<ActorStatProperty>().pokemon;
        if (playerPokemon == null || opponentPokemon == null) return;

        // Get each Pokemon ability list
        playerAbilities = playerPokemon.abilities;
        opponentAbilities = opponentPokemon.abilities;
        if (playerAbilities == null || opponentAbilities == null) return;

        // Apply player Pokemon abilities to action buttons
        for (int i = 0; i < playerAbilities.Count; i++)
        {
            playerAbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerAbilities[i].abilityName;
        }

        // If the player has not enough abilities(4), button will be deactivated.
        foreach (Button btn in playerAbilityButtons)
        {
            if (btn.GetComponentInChildren<TextMeshProUGUI>().text == "Button")
            {
                btn.gameObject.SetActive(false);
            }
        }
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
