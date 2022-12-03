using System.Collections;
using System.Collections.Generic;
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
    [Header("Manager Properties")]
    public List<Button> abilityButtons;

    [Header("Actor Propeties")]
    // TODO change it to specific class?
    public GameObject player;
    public List<Ability> playerAbilities;
    public GameObject opponent;
    public List <Ability> opponentAbilities;

    [Header("Battle Properties")]
    public TextMeshProUGUI battleMessageText;
    public string battleMessage;

    [Header("Debuggin Purpose")]
    [SerializeField] private bool _isInBattle = true;
    [SerializeField] private bool _isPlayerTurn = true;
    [SerializeField] private bool _isOpponentTurn = false;
    [SerializeField] private bool _isDebugging = false;    
   

    private void Start()
    {        
        // TODO - get actor each gameobjects
        player = GameObject.Find("Player"); // placeholder
        opponent = GameObject.Find("Opponent"); // placeholder
        
        if (player == null || opponent == null) return;

        //playerAbilities = player.abilities;
        //opponentAbilities = opponent.abilities;

        if (playerAbilities == null || opponentAbilities == null) return;

        for (int i = 0; i < playerAbilities.Count; i++)
        {
            abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = playerAbilities[i].abilityName;
        }

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
        if (!_isPlayerTurn && _isInBattle)
        {
            StartCoroutine(UseOpponentAbility());
            _isOpponentTurn = true;
            ToggleActorTurn();
        }
        else if ((_isPlayerTurn && !_isOpponentTurn) || !_isInBattle)
        {
            StopCoroutine(UseOpponentAbility());
        }
    }

    IEnumerator UseOpponentAbility()
    {
        yield return new WaitForSeconds(2.0f);

        int randNum = Random.Range(0, opponentAbilities.Count);
        //TODO ifHP heal;
        opponentAbilities[randNum].UseAbility(opponent.gameObject, player.gameObject);
        _isOpponentTurn = false;

        if (_isDebugging) Debug.Log($"End opponent turn. _isOpponentTurn = {_isOpponentTurn}");
        yield return null;
    }

    public void EndBattle()
    {
        _isInBattle = false;

        if (_isDebugging) Debug.Log($"Battle End. _isInBattle = {_isInBattle}");
    }

    private void ToggleActorTurn()
    {
        _isPlayerTurn = !_isPlayerTurn;

        if (_isDebugging) Debug.Log($"_isPlayerTurn = {_isPlayerTurn}");
    }


    #region Ability Button Functions
    public void OnClickSkill1Button()
    {
        if (_isPlayerTurn && _isInBattle && !_isOpponentTurn)
        {
            if (playerAbilities[0] != null)
            {
                playerAbilities[0].UseAbility(player.gameObject, opponent.gameObject);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill2Button()
    {
        if (_isPlayerTurn && _isInBattle && !_isOpponentTurn)
        {
            if (playerAbilities[1] != null)
            {
                playerAbilities[1].UseAbility(player.gameObject, opponent.gameObject);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill3Button()
    {
        if (_isPlayerTurn && _isInBattle && !_isOpponentTurn)
        {
            if (playerAbilities[2] != null)
            {
                playerAbilities[2].UseAbility(player.gameObject, opponent.gameObject);
            }
            ToggleActorTurn();
        }
    }
    public void OnClickSkill4Button()
    {
        if (_isPlayerTurn && _isInBattle && !_isOpponentTurn)
        {
            if (playerAbilities[3] != null)
            {
                playerAbilities[3].UseAbility(player.gameObject, opponent.gameObject);
            }
            ToggleActorTurn();
        }
    }
    #endregion
}
