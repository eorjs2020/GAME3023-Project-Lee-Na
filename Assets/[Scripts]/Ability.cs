using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Pokemon/Ability")]
public class Ability : ScriptableObject
{
    [Header("Ability Properties")]
    public string abilityName;
    [Range(0f, 1f)]
    public float chanceToSucceedRate;
    [Range(0f, 1f)]
    public float chanceToGainRate;

    // TODO - add visual effect
    // TODO - add sound
    // TODO - add animation

    [Header("Effect Properties")]
    public List<AbilityEffect> effects;

    /// <summary>
    /// Use this ability.
    /// </summary>
    /// <param name="casterGO"></param>
    /// <param name="targetGO"></param>
    public void UseAbility(GameObject casterGO, GameObject targetGO)
    {
        Debug.Log($"{casterGO.name} used an ability(${abilityName}) to {targetGO.name}");

        foreach (AbilityEffect effect in effects)
        {
            ApplyEffect(casterGO, targetGO, effect);
        }
    }

    public void ApplyEffect(GameObject casterGO, GameObject targetGO, AbilityEffect effect)
    {
        string battleLog = "";
        // TODO Send log to message system

        switch (effect.effectType)
        {
            case AbilityEffectType.FLEE:
                battleLog = $"{casterGO.name} : {casterGO.name} has fled from the battle.";
                break;
            case AbilityEffectType.MELEEATTACK:
                battleLog = $"{casterGO.name} : {targetGO.name} got damaged '{effect.weightNumber}' points.";
                break;
            case AbilityEffectType.RANGEATTACK:
                battleLog = $"{casterGO.name} : {targetGO.name} got damaged '{effect.weightNumber}' points.";
                break;
            case AbilityEffectType.HEAL:
                battleLog = $"{casterGO.name} : {casterGO.name} heald '{effect.weightNumber}' points";
                break;
            case AbilityEffectType.NONE:
                break;
            default:
                break;
        }
        Debug.Log(battleLog);
    }
}
