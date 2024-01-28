using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    #region Editable Variables
    [Header("Editable Variables")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    #endregion

    public event EventHandler OnDie; //Used for whenever a player is killed.
    public event EventHandler OnDamaged; //Used for any events that happen when player is damaged.
    public event EventHandler OnHealed; //Used for anything that happens when a player gets healed
    private void Awake() {
        currentHealth = maxHealth;
    }


    //Need way to modify current health
    public void TakeDamage(float damageAmount) {
        currentHealth -= damageAmount;

        if(currentHealth < 0) {
            currentHealth = 0;
        }

        //Need to update Player UI when damage taken/healed
        OnDamaged?.Invoke(this, EventArgs.Empty); //cause any event subscribed to activate when hit.

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
        OnDie?.Invoke(this, EventArgs.Empty);
    }
}
