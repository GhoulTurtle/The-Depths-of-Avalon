//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(fileName = "Fire Damage", menuName = "Damage Type/Fire Damage")]
public class FireDamageSO : DamageTypeSO{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        if(!healthSystem.IsAlive) return;
        
        if(healthSystem.TryGetComponent(out Character character) && statusEffect != null){
            statusEffect.SetStatus(Status.Burned);
            character.ApplyStatusEffectToCharacter(statusEffect, damageSource);
        }

        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
