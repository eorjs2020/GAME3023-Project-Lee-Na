using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public List<Ability> learnableAbility;
    public GameObject chooseAbility;
    public List<Button> learnalbeSlot;
    
    public GameObject chooseWhere;
    public List<Button> PlayerSlot;
    public Ability currentAbility;
        
    // Start is called before the first frame update
    private void Start()
    {
        UISetting();
    }

    public void Interact()
    {
        UISetting();
        chooseAbility.SetActive(true);       
    }
    public void ChooseClicked(Button button)
    {
        string name = button.name;
        int temp = 0;
        temp = int.Parse(name);
        currentAbility = learnableAbility[temp];
        chooseAbility.SetActive(false);
        chooseWhere.SetActive(true);
    }

    public void SetAbility(Button button)
    {        
        int temp = int.Parse(button.name);
        DataBase.Instance.GetPlayerPokemon().Abilities[temp] = currentAbility;
        chooseWhere.SetActive(false);        
    }

    private void  UISetting()
    {
        for (int i = 0; i < learnableAbility.Count; ++i)
        {
            learnalbeSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = learnableAbility[i].name;
        }
        for (int i = 0; i < PlayerSlot.Count; ++i)
        {
            if (DataBase.Instance.GetPlayerPokemon().Abilities[i] != null)
                PlayerSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = DataBase.Instance.GetPlayerPokemon().Abilities[i].name;
            else
                PlayerSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = "None";
        }
    }
}
