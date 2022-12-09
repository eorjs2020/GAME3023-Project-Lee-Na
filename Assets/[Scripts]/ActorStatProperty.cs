using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorStatProperty : MonoBehaviour
{
    [Header("Battle Properties")]
    public int currentHealth = 100;
    public int maxHealth = 100;
    public List<Ability> abilities;
    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;
    public Pokemon pokemon;

    private void Start()
    {
        maxHealthText.text = " / " + maxHealth.ToString();
        currentHealthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        currentHealthText.text = currentHealth.ToString();
    }
}
