//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(fileName = "Ice Damage", menuName = "Damage Type/Ice Damage")]
public class IceDamageSO : DamageTypeSO
{
    // Instant Damage.
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        if(!healthSystem.IsAlive) return;

        if(healthSystem.TryGetComponent(out Character character) && statusEffect != null){
            statusEffect.SetStatus(Status.Slow);
            character.ApplyStatusEffectToCharacter(statusEffect, damageSource);
        }

        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
