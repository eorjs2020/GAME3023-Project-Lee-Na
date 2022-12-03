using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAbilityEffect", menuName = "Pokemon/AbilityEffect")]
public class AbilityEffect : ScriptableObject
{
    public string effectName;
    public AbilityEffectType effectType;
    [Tooltip("This value is for effect. Either healing or damage with this number.")]
    [Range(0, 100)]
    public int weightNumber;
    public string effectDescription;
}