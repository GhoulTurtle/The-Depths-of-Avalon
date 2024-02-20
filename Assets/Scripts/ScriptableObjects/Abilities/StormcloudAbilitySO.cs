//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/State Change/Stormcloud", fileName = "NewStormcloudAbilitySO")]
public class StormCloudAbilitySO : AbilitySO{
    [Header("Stormcloud Variables")]
    [Tooltip("The amount of time the caster can be in storm cloud form")]
    [SerializeField] private float stormCloudTime = 3f;
    [Tooltip("The multipler that affects the movement speed of the caster while in stormcloud form.")]
    [SerializeField] private float movementSpeedMultipler = 1.2f; 
    [SerializeField] private string cloudLayerMaskName;

    private const string playerLayerMashName = "Player";

    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
         if(caster.TryGetComponent(out HealthSystem casterHealth)){
            //Become invincible for ability duration
            casterHealth.SetIsInvincible(true);
        }

        if(caster.TryGetComponent(out PlayerMovement playerMovement)){
            playerMovement.SetMovementSpeed(playerMovement.CurrentMovementSpeed * movementSpeedMultipler);
        }

        if(caster.TryGetComponent(out PlayerInputHandler playerInputHandler)){
            playerInputHandler.SetDisableAbilityInput(true);
        }

        if(caster.character != null){
            //Clear all effects on the character
            caster.character.RemoveAllEffects();
        }

        int cloudLayerValue = LayerMask.NameToLayer(cloudLayerMaskName);

        caster.gameObject.layer = cloudLayerValue;

        caster.StartAbilityCoroutine(CloudFormCoroutine(caster));
    }

    private IEnumerator CloudFormCoroutine(Caster caster){
        yield return new WaitForSecondsRealtime(stormCloudTime);
        if(caster.TryGetComponent(out HealthSystem casterHealth)){
            //Become invincible for ability duration
            casterHealth.SetIsInvincible(false);
        }

        if(caster.TryGetComponent(out PlayerMovement playerMovement)){
            playerMovement.ResetMovementSpeed();
        }

        if(caster.TryGetComponent(out PlayerInputHandler playerInputHandler)){
            playerInputHandler.SetDisableAbilityInput(false);
        }

        int playerLayerValue = LayerMask.NameToLayer(playerLayerMashName);

        caster.gameObject.layer = playerLayerValue;
    }
}
