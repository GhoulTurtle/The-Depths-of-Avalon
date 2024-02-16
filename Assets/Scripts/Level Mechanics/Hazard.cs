//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System.Collections;
using UnityEngine;

public class Hazard : MonoBehaviour{
	[Header("Hazard Status Variables")]
	[Tooltip("The amount of time that the hazard will wait till it apply status to the affected objects.")]
	[SerializeField] private float recoverTime = 2.5f;
	[Tooltip("The status effect this this hazard applies.")]
	[SerializeField] private StatusEffect hazardStatusEffect;
	
	[Header("Required References")]
	[SerializeField] private LayerMask affectedLayers;

	private bool onCooldown = false;

	private void OnDestroy() {
		StopAllCoroutines();
	}

	private void OnTriggerStay(Collider other) {
		if(onCooldown) return;

		if((affectedLayers.value & 1 << other.gameObject.layer) != 0) {			
			HazardStatus(other.gameObject);

			StartCoroutine(HazardWaitCoroutine());
		}
	}

	private void HazardStatus(GameObject affectedObject){
		if(affectedObject.TryGetComponent(out Character character)){
			if(hazardStatusEffect.Status == Status.None){
				Debug.LogWarning(gameObject.name + " hazard does not have the Status effect set on the StatusEffect.");
				return;
			}

			character.ApplyStatusEffectToCharacter(hazardStatusEffect, transform);
		}
	}

	private IEnumerator HazardWaitCoroutine(){
		onCooldown = true;
		yield return new WaitForSecondsRealtime(recoverTime);
		onCooldown = false;
	}
}
