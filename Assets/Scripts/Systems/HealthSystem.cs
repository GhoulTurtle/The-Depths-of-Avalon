using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

#region Editable Variables
    [Header("Editable Variables")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    [SerializeField] private DamageTypeSO requiredDamageType;
#endregion

#region Events
    public event EventHandler OnDie; //Used for whenever a player is killed.
    public event EventHandler<DamagedEventArgs> OnDamaged; //Used for any events that happen when player is damaged.
    public event EventHandler OnHealed; //Used for anything that happens when a player gets healed

    public class DamagedEventArgs : EventArgs {
        public DamageTypeSO damageTypeSO;
        public float damageAmount;
        public Transform damageSourceGO;
        public DamagedEventArgs(DamageTypeSO _damageTypeSO, float _damageAmount, Transform _damageSource) {
            damageTypeSO = _damageTypeSO;
            damageAmount = _damageAmount;
            damageSourceGO = _damageSource;
        }
    }
#endregion

    [SerializeField] private bool isAlive;

    private void Awake() {
        currentHealth = maxHealth;
        isAlive = true;
    }

    //Need way to modify current health
    public void TakeDamage(DamageTypeSO damageType, float damageAmount, Transform damageSource) {
        if(requiredDamageType != null && damageType != requiredDamageType) {
            return;
        } if(!isAlive) {
            return;
        }

        currentHealth -= damageAmount;

        if(currentHealth < 0) {
            currentHealth = 0;
        }

        //Need to update Player UI when damage taken/healed
        OnDamaged?.Invoke(this, new DamagedEventArgs(damageType, damageAmount, damageSource)); //cause any event subscribed to activate when hit.

        if(currentHealth == 0) {
            Die();
        }
    }

    public void Heal(float healAmount) {
        currentHealth += healAmount;

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
            return;
        }

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    //Need way to trigger OnDie event when health <= 0
    private void Die() {
        isAlive = false;
        OnDie?.Invoke(this, EventArgs.Empty);
    }
 }
