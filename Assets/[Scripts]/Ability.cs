using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Pokemon/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public List<AbilityEffect> effects;
    // TODO - add visual efect
    // TODO - add sounds
    // TODO - add animation

    public void UseAbility(GameObject casterGO, GameObject targetGO)
    {
        Debug.Log($"{casterGO.name} used an ability(${abilityName}) to {targetGO.name}");

        foreach (AbilityEffect effect in effects)
        {

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
                battleLog = $"{casterGO.name} : {targetGO.name} got {effect.weightNumber} damages.";
                break;
            case AbilityEffectType.HEAL:
                battleLog = $"{casterGO.name} : {casterGO.name} heald {effect.weightNumber} points";
                break;
            case AbilityEffectType.NONE:
                break;
            default:
                break;
        }
        Debug.Log(battleLog);
    }
}
