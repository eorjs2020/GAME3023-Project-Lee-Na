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
    public string abilityDescription;
    [Range(0f, 1f)] [SerializeField] private float chanceToGainAbility = 0.3f;

    // TODO - add visual effect
    // TODO - add sound
    // TODO - add animation

    [Header("Effect Properties")]
    public List<AbilityEffect> effects;

    public void UseAbility(ActorStatProperty caster, ActorStatProperty target)
    {
        Debug.Log($"{caster.name} used an ability(${abilityName}) to {target.name}");
        BattleMessageManager.Instance.SendTextMessage($"{caster.name} used an ability(${abilityName}) to {target.name}");

        // Using all effects in ability
        foreach (AbilityEffect effect in effects)
        {
            ApplyEffect(caster, target, effect);
        }
    }

    public void ApplyEffect(ActorStatProperty caster, ActorStatProperty target, AbilityEffect effect)
    {
        string battleLog = "";
        // TODO Send log to message system

        switch (effect.effectType)
        {
            case AbilityEffectType.FLEE:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{caster.name} : {caster.name} has fled from the battle.";
                    GetFlee(caster);
                } else {
                    battleLog = $"{caster.name} : {caster.name} failed to flee from the battle.";
                }
                break;
            case AbilityEffectType.ATTACK:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{caster.name} : {target.name} got damaged '{effect.strengthNumber}' points.";
                    GetDamaged(target, effect.strengthNumber);
                }
                else
                {
                    battleLog = $"{caster.name} : {caster.name} failed to attack.";
                }
                break;
            case AbilityEffectType.HEAL:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{caster.name} : {caster.name} heald '{effect.strengthNumber}' points";
                    GetHeald(caster, effect.strengthNumber);
                } else {
                    battleLog = $"{caster.name} : {caster.name} failed to heal itself.";
                }
                break;
            case AbilityEffectType.STUN:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{caster.name} : {target.name} is stunned!";
                    GetStunned(target);
                } else  {
                    battleLog = $"";
                }
                break;
            case AbilityEffectType.NONE:
                break;
            default:
                break;
        }
        Debug.Log(battleLog);
    }

    public void GetFlee(ActorStatProperty target)
    {
        HideTarget(target);
        Singleton<BattleSystem>.Instance.EndBattle();
    }

    public void GetStunned(ActorStatProperty target)
    {
        target.IsStunned = true;
    }

    public void GetDamaged(ActorStatProperty target, int damage)
    {
        target.CurrentHealth -= damage;

        if (target.CurrentHealth <= 0)
        {
            HideTarget(target);
            Singleton<BattleSystem>.Instance.EndBattle();
            target.CurrentHealth = target.MaxHealth;
        }
    }
    public void GetHeald(ActorStatProperty target, int healdPoints)
    {
        if (target.CurrentHealth <= target.MaxHealth)
        {
            target.CurrentHealth += healdPoints;
        }
    }

    private static void HideTarget(ActorStatProperty target)
    {
        target.gameObject.SetActive(false);
        target.GetMaxHPText().gameObject.SetActive(false);
        target.GetCurrentHPText().gameObject.SetActive(false);
    }
}
