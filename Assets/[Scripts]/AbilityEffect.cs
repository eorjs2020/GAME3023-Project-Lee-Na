using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityEffect", menuName = "Pokemon/AbilityEffect")]
public class AbilityEffect : ScriptableObject
{
    public string effectName;
    public AbilityEffectType effectType;
    [Range(0, 100)]
    public int weightNumber;
    [Range(0f, 1f)]
    public float chanceRate;
    public string effectDescription;
}
