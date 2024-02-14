//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

public abstract class DamageTypeSO : ScriptableObject {
    public abstract void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource);
}
