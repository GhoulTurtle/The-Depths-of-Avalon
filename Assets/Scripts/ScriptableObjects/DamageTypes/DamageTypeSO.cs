using UnityEngine;

public abstract class DamageTypeSO : ScriptableObject {
    public abstract void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource);
}
