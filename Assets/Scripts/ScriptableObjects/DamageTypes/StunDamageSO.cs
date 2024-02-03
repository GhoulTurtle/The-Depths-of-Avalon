using UnityEngine;

[CreateAssetMenu(fileName = "Stun Damage", menuName = "Damage Type/Stun Damage")]
public class StunDamageSO : DamageTypeSO{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect) {
        if(healthSystem.TryGetComponent(out Character character) && statusEffect != null){
            statusEffect.SetStatus(Status.Stun);
            character.ApplyStatusEffectToCharacter(statusEffect);
        }

        healthSystem.TakeDamage(this, damageAmount);
    }
}
