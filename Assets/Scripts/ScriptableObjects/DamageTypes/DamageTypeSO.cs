using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageTypeSO : ScriptableObject {
    public abstract void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect);
}
