//Last Editor: Caleb Richardson
//Last Edited: Feb 16
using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

#region Editable Variables
    [Header("Editable Variables")]
    [SerializeField] private bool isInvincible = false;
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
        public Transform damageSource;
        public DamagedEventArgs(DamageTypeSO _damageTypeSO, float _damageAmount, Transform _damageSource) {
            damageTypeSO = _damageTypeSO;
            damageAmount = _damageAmount;
            damageSource = _damageSource;
        }
    }
#endregion

#region References
    private Character character;
    private BurnJob currentBurnJob;
#endregion

    public bool IsAlive {get; private set;}

    private void Awake() {
        currentHealth = maxHealth;
        IsAlive = true;
    }

    private void Start() {
        if(TryGetComponent(out character)){
            character.OnStatusEffectApplied += StatusEffectApplied;
            character.OnStatusEffectFinished += StatusEffectFinished;
        }
    }

    private void OnDestroy() {
        if(character != null){
            character.OnStatusEffectApplied -= StatusEffectApplied;
            character.OnStatusEffectFinished -= StatusEffectFinished;
            StopAllCoroutines();
        }
    }

    //Need way to modify current health
    public void TakeDamage(DamageTypeSO damageType, float damageAmount, Transform damageSource) {
        if(requiredDamageType != null && damageType != requiredDamageType) {
            return;
        } if(!IsAlive) {
            return;
        }

        if(!isInvincible){
            currentHealth -= damageAmount;
        }

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
        if(!IsAlive) {
            IsAlive = true;
        }
        currentHealth += healAmount;
        

        if(currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }

        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void Respawn(){
        IsAlive = true;
        currentHealth = maxHealth;
    }

    public void SetIsInvincible(bool _isInvincible){
        isInvincible = _isInvincible;
    }

    //Need way to trigger OnDie event when health <= 0
    public void Die() {
        if(!isInvincible){
            IsAlive = false;
        } 

        if(currentBurnJob != null){
            currentBurnJob.FinishedBurning();
            currentBurnJob = null;
        }

        OnDie?.Invoke(this, EventArgs.Empty);
    }

    private void StatusEffectApplied(object sender, Character.StatusEffectAppliedEventArgs e){
        if(e.abilityEffect.Status != Status.Burned) return;
        currentBurnJob = new BurnJob(this);
        StartCoroutine(currentBurnJob.BurnDOTCorutine());
    }

    private void StatusEffectFinished(object sender, Character.StatusEffectAppliedEventArgs e){
        if(e.abilityEffect.Status != Status.Burned || currentBurnJob == null) return;
        currentBurnJob.FinishedBurning();
        currentBurnJob = null;
    }

    public float GetMaxHealth() {
        return maxHealth;
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }
 }