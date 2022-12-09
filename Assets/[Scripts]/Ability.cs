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
        BattleMessageManager.Instance.SendTextMessage($"{caster.PokemonName.ToUpper()} used an ability ({abilityName.ToUpper()}) to {target.PokemonName.ToUpper()}", caster.PokemonName);

        // Using all effects in ability
        foreach (AbilityEffect effect in effects)
        {
            ApplyEffect(caster, target, effect);
        }
    }

    public void ApplyEffect(ActorStatProperty caster, ActorStatProperty target, AbilityEffect effect)
    {
        string battleLog = "";

        switch (effect.effectType)
        {
            case AbilityEffectType.FLEE:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{caster.PokemonName.ToUpper()} has succesfully fled from the battle.";
                    GetFlee(caster);
                } else {
                    battleLog = $"{caster.PokemonName.ToUpper()} failed to flee from the battle.";
                }
                break;
            case AbilityEffectType.ATTACK:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    //TODO Animation, Sound_FX
                    battleLog = $"{target.PokemonName.ToUpper()} got damaged '{effect.strengthNumber}' points.";
                    GetDamaged(target, effect.strengthNumber);
                }
                else
                {
                    battleLog = $"{caster.PokemonName.ToUpper()} failed to attack {target.PokemonName.ToUpper()}.";
                }
                break;
            case AbilityEffectType.HEAL:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    //TODO PaticlePlay, Sound_FX
                    battleLog = $"{caster.PokemonName.ToUpper()} heald '{effect.strengthNumber}' points";
                    GetHeald(caster, effect.strengthNumber);
                } else {
                    battleLog = $"{caster.PokemonName.ToUpper()} failed to heal itself.";
                }
                break;
            case AbilityEffectType.STUN:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    battleLog = $"{target.PokemonName.ToUpper()} is stunned!.";
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

        BattleMessageManager.Instance.SendTextMessage(battleLog, caster.PokemonName);
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
