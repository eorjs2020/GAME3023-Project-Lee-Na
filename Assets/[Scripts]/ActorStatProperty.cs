using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityNum
{
    IDLE,
    ATTACK,
    HEAL
}

public class ActorStatProperty : MonoBehaviour
{
    [Header("Actor Stat Properties")]
    [SerializeField] private string pokemonName;
    [SerializeField] private TextMeshProUGUI maxHealthText;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private bool isStunned = false;
    [SerializeField] private bool isFleed = false;
    private Animator animator;
    public string animatorParameter = "BattleAnimation";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public Animator Animator
    {
        get { return animator; }
    }

    public string PokemonName
    {
        get { return pokemonName; }
        set { pokemonName = value; }
    }

    public int CurrentHealth
    {
        get { return currentHealth; }
        set { 
            currentHealth = value;
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

    public bool IsStunned
    {
        get { return isStunned; }
        set { isStunned = value; }
    }

    public bool IsFleed
    {
        get { return isFleed; }
        set { isFleed = value; }
    }

    public void AnimationReset()
    {
        animator.SetInteger(animatorParameter, 0);
    }
    
    public void PlayAnimation(AbilityNum abitly)
    {
        animator.SetInteger(animatorParameter, (int)abitly);
    }

    public TextMeshProUGUI GetMaxHPText() { return maxHealthText; }
    public TextMeshProUGUI GetCurrentHPText() { return currentHealthText; }
}
