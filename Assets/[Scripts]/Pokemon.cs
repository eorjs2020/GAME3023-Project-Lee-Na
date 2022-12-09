using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/New Pokemon")]
public class Pokemon : ScriptableObject
{    
    [SerializeField] private string name;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
   
    [SerializeField] private int maxHp;
    [SerializeField] private int currentHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    public List<Ability> abilities;
    public int abilityNum = 4;
    public string GetName
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }
    public int CurrentHp
    {
        get { return currentHp; }
        set { currentHp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int Attack
    {
        get { return attack; }
    }    
    public int Defence
    {
        get { return defense; }
    }  
}
