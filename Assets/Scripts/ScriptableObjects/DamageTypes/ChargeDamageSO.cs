//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(fileName = "Charge Damage", menuName = "Damage Type/Charge Damage")]
public class ChargeDamageSO : DamageTypeSO{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        if(!healthSystem.IsAlive) return;
        if(healthSystem.TryGetComponent(out Character character) && statusEffect != null){
            statusEffect.SetStatus(Status.Knockback);
            character.ApplyStatusEffectToCharacter(statusEffect, damageSource);
        }
        
        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
