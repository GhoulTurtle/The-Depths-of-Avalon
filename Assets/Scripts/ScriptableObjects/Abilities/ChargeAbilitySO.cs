using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack/Charge Attack", fileName = "NewChargeAbilitySO")]
public class ChargeAbilitySO : AbilitySO{
    [Header("Charge Ability Variables")]
    [Tooltip("The charge duration, this effects how long the invincibilty/anti-gravity of the ability lasts.")]
    [SerializeField] private float chargeDuration;
    [Tooltip("The charge strength, this effects how powerful the dash forward is.")]
    [SerializeField] private float chargeStrength;

    [Header("Charge Damage Variables")]
    [Tooltip("How big is the range of the collider sphere to check for target?")]
    public float ChargeRangeRadius;
    [Tooltip("The offset of the sphere check from the caster. Bigger caster = Bigger Offset")]
    public float ChargeCheckOffset;
    [Tooltip("The maximum amount of targets the melee can deal damage to."), Range(10, 30)]
    public int MaxTargetsHit;

    private Collider[] targetColliders;

    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
        if(caster.TryGetComponent(out HealthSystem casterHealth)){
            //Become invincible for dash duration
            casterHealth.SetIsInvincible(true);
        }

        if(caster.TryGetComponent(out PlayerMovement playerMovement)){
            playerMovement.SetPlayerGravity(0);
            playerMovement.AddExternalForce(caster.transform.forward, chargeStrength);
        }

        caster.StartAbilityCoroutine(ChargeCoroutine(caster));
    } 

    private IEnumerator ChargeCoroutine(Caster caster){
        //Start the damageDealer
        IEnumerator chargeDamageCorountine = ChargeDamageCoroutine(caster);
        
        caster.StartAbilityCoroutine(chargeDamageCorountine);

        yield return new WaitForSecondsRealtime(chargeDuration);
        //Stop the damageDealer
        caster.StopAbilityCoroutine(chargeDamageCorountine);

        if(caster.TryGetComponent(out HealthSystem casterHealth)){
            casterHealth.SetIsInvincible(false);
        }

        if(caster.TryGetComponent(out PlayerMovement playerMovement)){
            playerMovement.ResetPlayerGravity();
        }
    }

    private IEnumerator ChargeDamageCoroutine(Caster caster){
        //Set the maximum amount of targets the melee can hit
        targetColliders = new Collider[MaxTargetsHit];
        
        //Everyframe calculate in front of the caster to deal damage
        while(true){
            var meleePos = caster.transform.position + caster.transform.forward * ChargeCheckOffset;
            
            if(DrawAbilityGizmos) caster.DebugAbility(GizmosColor, GizmosShape, meleePos, Vector3.zero, ChargeRangeRadius);
		
            //Do a overlap sphere in front of the caster if results buffer doesn't returns 0 then calculate damage
            if(Physics.OverlapSphereNonAlloc(meleePos, ChargeRangeRadius, targetColliders, caster.TargetLayerMask) != 0){
                //Go through each target and attempt to deal damage
                foreach (Collider target in targetColliders){
                    if(target == null) continue;
                    if(!target.TryGetComponent(out HealthSystem healthSystem)) continue;
                
                    var randomDamage = Random.Range(AbilityDamageAmount.minValue, AbilityDamageAmount.maxValue);

                    if(AbilityDamageType != null){
                        AbilityDamageType.DealDamage(healthSystem, randomDamage, AbilityStatusEffect, caster.transform);
                    }
                    else{
                        healthSystem.TakeDamage(AbilityDamageType, randomDamage, caster.transform);
                    }
                }
            }

            yield return null;
        }
    }
}
