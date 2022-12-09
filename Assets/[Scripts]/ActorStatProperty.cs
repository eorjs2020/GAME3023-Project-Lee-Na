using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActorStatProperty : MonoBehaviour
{
    [Header("Actor Stat Properties")]
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
            Debug.Log(currentHealth);
            currentHealthText.text = currentHealth.ToString();
        }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set {
            maxHealth = value;
            maxHealthText.text = " / " + maxHealth.ToString();
        }
    }

    public Sprite SetPokemonSprite
    {
        get { return spriteRenderer.sprite; }
        set { spriteRenderer.sprite = value; }
    }
}
