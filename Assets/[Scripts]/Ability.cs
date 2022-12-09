using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Pokemon/Ability")]
public class Ability : ScriptableObject
{
    [Header("Ability Properties")]
    public string abilityName;
    public AbilityType abilityType;
    [Range(0f, 1f)] public float chanceToSucceedRate;
    public string abilityDescription;

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
    public void UseAbility(Pokemon casterGO, Pokemon targetGO)
    {
        Debug.Log($"{casterGO.name} used an ability(${abilityName}) to {targetGO.name}");

        // Using all effects in ability
        foreach (AbilityEffect effect in effects)
        {
            ApplyEffect(casterGO, targetGO, effect);
        }
    }

    public void ApplyEffect(Pokemon casterGO, Pokemon targetGO, AbilityEffect effect)
    {
        string battleLog = "";
        // TODO Send log to message system

        switch (effect.effectType)
        {
            case AbilityEffectType.FLEE:
                battleLog = $"{casterGO.name} : {casterGO.name} has fled from the battle.";
                Singleton<BattleSystem>.Instance.EndBattle();
                break;
            case AbilityEffectType.ATTACK:
                battleLog = $"{casterGO.name} : {targetGO.name} got damaged '{effect.strengthNumber}' points.";
                GetDamaged(targetGO, effect.strengthNumber);
                break;
            case AbilityEffectType.HEAL:
                battleLog = $"{casterGO.name} : {casterGO.name} heald '{effect.strengthNumber}' points";
                GetHeald(casterGO, effect.strengthNumber);
                break;
            case AbilityEffectType.STUN:
                if (Random.Range(0.0f, 100.0f) <= effect.strengthNumber)
                {
                    battleLog = $"{casterGO.name} : {targetGO.name} is stunned!";
                    GetStunned(targetGO);
                }
                break;
            case AbilityEffectType.NONE:
                break;
            default:
                break;
        }
        Debug.Log(battleLog);
    }

    public void GetStunned(Pokemon target)
    {
        
    }

    public void GetDamaged(Pokemon target, int damage)
    {
        target.CurrentHp -= damage;

        if (target.CurrentHp <= 0)
        {
            target.CurrentHp = target.MaxHp;
            Singleton<BattleSystem>.Instance.EndBattle();
        }
    }

    public void GetHeald(Pokemon target, int healdPoints)
    {
        if (target.CurrentHp <= target.MaxHp)
        {
            target.CurrentHp += healdPoints;
        }
    }
}
