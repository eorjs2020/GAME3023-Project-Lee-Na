using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityChangeSystem : MonoBehaviour
{
    [Header("System Properties")]
    public List<Button> currentAbilityButtons;
    public List<Ability> currentAbilities;
    public List<Button> gainedAbilityButtons;
    public List<Ability> gainedAbilities;
    public Button skipButton;
    public TextMeshProUGUI selectedAbilityDisplayText;
    public Ability selectedAbility;
    public int selectableAbilitycount = 1;

    private Pokemon playerPokemon;

    public void SetCurrentAbilityButtons(Pokemon pokemon)
    {
        playerPokemon = pokemon;
        currentAbilities = pokemon.abilities;
        for (int i = 0; i < currentAbilities.Count; i++)
        {
            if (currentAbilities[i] != null)
            {
                currentAbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentAbilities[i].abilityName;
            }
            else
            {
                currentAbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
            }
        }
    }

    public void SetGainedAbilityButtons(List<Ability> abilities)
    {
        gainedAbilities = abilities;
        for (int i = 0; i < gainedAbilityButtons.Count; i++)
        {
            if (i < gainedAbilities.Count && gainedAbilities[i] != null)
            {
                gainedAbilityButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = gainedAbilities[i].abilityName;
            }
            else
            {
                gainedAbilityButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void DiaplaySelectedAbility()
    {
        selectedAbilityDisplayText.text = selectedAbility.abilityName;
    }

    public void UpdateGainedAbilityButtons()
    {
        if (selectableAbilitycount <= 0)
        {
            for (int i = 0; i < gainedAbilityButtons.Count; i++)
            {
                gainedAbilityButtons[i].gameObject.SetActive(false);
            }

            skipButton.GetComponentInChildren<TextMeshProUGUI>().text = "End";
        }
    }

    public void EndAbilityChange()
    {

    }

    #region Ability Button Functions
    public void OnClickCurrentAbility1Button()
    {
        Debug.Log("OnClickCurrentAbility1Button");
        if (selectedAbility != null && selectableAbilitycount > 0)
        {
            playerPokemon.Abilities[0] = selectedAbility;
            currentAbilityButtons[0].GetComponentInChildren<TextMeshProUGUI>().text = selectedAbility.abilityName;
            selectableAbilitycount--;
            UpdateGainedAbilityButtons();
        }
    }
    public void OnClickCurrentAbility2Button()
    {
        Debug.Log("OnClickCurrentAbility2Button");
        if (selectedAbility != null && selectableAbilitycount > 0)
        {
            playerPokemon.Abilities[1] = selectedAbility;
            currentAbilityButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = selectedAbility.abilityName;
            selectableAbilitycount--;
            UpdateGainedAbilityButtons();
        }
    }
    public void OnClickCurrentAbility3Button()
    {
        Debug.Log("OnClickCurrentAbility3Button");
        if (selectedAbility != null && selectableAbilitycount > 0)
        {
            playerPokemon.Abilities[2] = selectedAbility;
            currentAbilityButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = selectedAbility.abilityName;
            selectableAbilitycount--;
            UpdateGainedAbilityButtons();
        }
    }
    public void OnClickCurrentAbility4Button()
    {
        Debug.Log("OnClickCurrentAbility4Button");
        if (selectedAbility != null && selectableAbilitycount > 0)
        {
            playerPokemon.Abilities[3] = selectedAbility;
            currentAbilityButtons[3].GetComponentInChildren<TextMeshProUGUI>().text = selectedAbility.abilityName;
            selectableAbilitycount--;
            UpdateGainedAbilityButtons();
        }
    }

    public void OnClickGainedAbility1Button()
    {
        selectedAbility = gainedAbilities[0];
        DiaplaySelectedAbility();
    }

    public void OnClickGainedAbility2Button()
    {
        selectedAbility = gainedAbilities[1];
        DiaplaySelectedAbility();
    }

    public void OnClickGainedAbility3Button()
    {
        selectedAbility = gainedAbilities[2];
        DiaplaySelectedAbility();
    }

    public void OnClickGainedAbility4Button()
    {
        selectedAbility = gainedAbilities[3];
        DiaplaySelectedAbility();
    }
    #endregion
}
