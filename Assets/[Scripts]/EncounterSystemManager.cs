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

public class EncounterSystemManager : Singleton<EncounterSystemManager>
{
    [Header("Encounter System Status")]
    [SerializeField] private bool isBattleEnd = false;

    [Header("Manager Properties")]
    public List<Button> abilityButtons;

    [Header("Actor Propeties")]
    public Pokemon playerPokemon;
    public List<Ability> playerAbilities;
    public Pokemon opponentPokemon;
    public List <Ability> opponentAbilities;

    [Header("Battle Properties")]
    public TextMeshProUGUI battleMessageText;
    public string battleMessage;

    [Header("Debuggin Purpose")]
    [SerializeField] private bool isPlayerTurn = true;
    [SerializeField] private bool isOpponentTurn = false;
    [SerializeField] private bool isDebugging = false;

    [Header("Ability Buttons")]
    public List<Button> buttons;

    private void Awake()
    {
        var obj = FindObjectsOfType<EncounterSystemManager>();
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
        playerPokemon = GameObject.Find("Player").GetComponent<Pokemon>();
        opponentPokemon = GameObject.Find("opponent").GetComponent<Pokemon>();
        
        if (playerPokemon == null || opponentPokemon == null) return;

        // Get each Pokemon ability list
        playerAbilities = playerPokemon.abilities;
        opponentAbilities = opponentPokemon.abilities;

        if (playerAbilities == null || opponentAbilities == null) return;

        // Apply player's Pokemon ability to buttons
        for (int i = 0; i < playerAbilities.Count; i++)
        {
            abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerAbilities[i].abilityName;
        }

        // If the player has not enough abilities, button will be deactivated.
        foreach (Button btn in abilityButtons)
        {
            if (btn.GetComponentInChildren<TextMeshProUGUI>().text == "Button")
            {
                btn.gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (!isPlayerTurn && isBattleEnd)
        {
            StartCoroutine(UseOpponentAbility());
            isOpponentTurn = true;
            ToggleActorTurn();
        }
        else if ((isPlayerTurn && !isOpponentTurn) || !isBattleEnd)
        {
            StopCoroutine(UseOpponentAbility());
        }
    }

    IEnumerator UseOpponentAbility()
    {
        yield return new WaitForSeconds(2.0f);

        //int randNum = Random.Range(0, opponentAbilities.Count);        
        //opponentAbilities[randNum].UseAbility(opponentPokemon, playerPokemon);

        SelectAbility().UseAbility(opponentPokemon, playerPokemon);

        isOpponentTurn = false;

        if (isDebugging) Debug.Log($"End opponent turn. _isOpponentTurn = {isOpponentTurn}");
        yield return null;
    }

    private Ability SelectAbility()
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
        Debug.Log($"Opponent chose a random ability.");
        return opponentAbilities[Random.Range(0, opponentAbilities.Count)];
    }
    public void GetDamaged(Pokemon target, int damage)
    {
        target.CurrentHp -= damage;

        if (target.CurrentHp <= 0)
        {
            target.CurrentHp = target.MaxHp;
            EndBattle();
        }
    }

    public void GetHeald(Pokemon target, int healdPoints)
    {
        if (target.CurrentHp <= 100)
        {
            target.CurrentHp += healdPoints;
        }
    }

    public void EndBattle()
    {
        isBattleEnd = true;

        if (isDebugging) Debug.Log($"Battle End. _isInBattle = {isBattleEnd}");
    }

    private void ToggleActorTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        if (isDebugging) Debug.Log($"_isPlayerTurn = {isPlayerTurn}");
    }


    #region Ability Button Functions
    public void OnClickSkill1Button()
    {
        if (isPlayerTurn && isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[0] != null)
            {
                playerAbilities[0].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill2Button()
    {
        if (isPlayerTurn && isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[1] != null)
            {
                playerAbilities[1].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill3Button()
    {
        if (isPlayerTurn && isBattleEnd && !isOpponentTurn)
        {
            if (playerAbilities[2] != null)
            {
                playerAbilities[2].UseAbility(playerPokemon, opponentPokemon);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill4Button()
    {
        if (isPlayerTurn && isBattleEnd && !isOpponentTurn)
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
