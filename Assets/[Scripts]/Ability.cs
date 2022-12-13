using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Pokemon/Ability")]
public class Ability : ScriptableObject
{
    [Header("Ability Properties")]
    public string abilityName;
    public AbilityType abilityType;
    public string abilityDescription;
    [Range(0f, 1f)] [SerializeField] public float chanceToGainAbility = 0.3f;

    [Header("Effect Properties")]
    public List<AbilityEffect> effects;



    public void UseAbility(ActorStatProperty caster, ActorStatProperty target)
    {
        BattleMessageManager.Instance.SendTextMessage($"{caster.PokemonName.ToUpper()} used an ability ({abilityName.ToUpper()}) to {target.PokemonName.ToUpper()}", caster.PokemonName);

        // Using all effects in ability
        foreach (AbilityEffect effect in effects)
        {
            if (Singleton<BattleSystem>.Instance.isBattleEnd) return;

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
                    GetFlee(caster, target);
                } else {
                    battleLog = $"{caster.PokemonName.ToUpper()} failed to flee from the battle.";
                }
                break;
            case AbilityEffectType.ATTACK:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    caster.PlayAnimation(AbilityNum.ATTACK);
                    SoundManager.Instance.PlaySoundFX(Sound.ATTACK, Chanel.BATTLE_SOUND_FX);
                    battleLog = $"{target.PokemonName.ToUpper()} got damaged '{effect.strengthNumber}' points.";
                    GetDamaged(caster, target, effect.strengthNumber);
                }
                else
                {
                    battleLog = $"{caster.PokemonName.ToUpper()} failed to attack {target.PokemonName.ToUpper()}.";
                }
                break;
            case AbilityEffectType.HEAL:
                if (Random.Range(0.0f, 1.0f) <= effect.chanceToSucceedRate)
                {
                    caster.PlayAnimation(AbilityNum.HEAL);
                    SoundManager.Instance.PlaySoundFX(Sound.HEAL, Chanel.BATTLE_SOUND_FX);
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
        
        if (target.CurrentHealth <= 0)
        {
            Singleton<BattleMessageManager>.Instance.SendTextMessage($"{caster.PokemonName.ToUpper()} won the battle.", caster.PokemonName);
            Singleton<BattleSystem>.Instance.EndBattle(target, caster);
            target.CurrentHealth = target.MaxHealth;
            HideTarget(target);
        }
    }

    public void GetFlee(ActorStatProperty caster, ActorStatProperty target)
    {
        Singleton<BattleMessageManager>.Instance.SendTextMessage($"{target.PokemonName.ToUpper()} won the battle.", caster.PokemonName);
        Singleton<BattleSystem>.Instance.EndBattle(caster, target);
        HideTarget(caster);
    }

    public void GetStunned(ActorStatProperty target)
    {
        target.IsStunned = true;
    }

    public void GetDamaged(ActorStatProperty caster, ActorStatProperty target, int damage)
    {
        target.CurrentHealth -= damage;

        if (target.CurrentHealth <= 0)
        {
            target.CurrentHealth = 0;
        }
    }
    public void GetHeald(ActorStatProperty target, int healdPoints)
    {
        if (target.CurrentHealth <= target.MaxHealth)
        {
            target.CurrentHealth += healdPoints;
            
            if (target.CurrentHealth > target.MaxHealth)
            {
                target.CurrentHealth = target.MaxHealth;
            }
        }
    }

    private static void HideTarget(ActorStatProperty target)
    {
        target.gameObject.SetActive(false);
        target.GetMaxHPText().gameObject.SetActive(false);
        target.GetCurrentHPText().gameObject.SetActive(false);
    }


}
