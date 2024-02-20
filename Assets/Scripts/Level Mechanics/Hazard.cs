//Last Editor: Caleb Richardson
//Last Edited: Feb 15

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour{
	[Header("Hazard Status Variables")]
	[Tooltip("The amount of time that the hazard will wait till it apply status to the affected objects.")]
	[SerializeField] private float recoverTime = 2.5f;
	[Tooltip("The status effect this this hazard applies.")]
	[SerializeField] private StatusEffect hazardStatusEffect;
	
	[Header("Required References")]
	[SerializeField] private LayerMask affectedLayers;

	private Dictionary<GameObject, IEnumerator> affectedObjectDictionary = new Dictionary<GameObject, IEnumerator>(); 

	private void OnDestroy() {
		affectedObjectDictionary.Clear();
		StopAllCoroutines();
	}

	private void OnTriggerStay(Collider other) {
		if((affectedLayers.value & 1 << other.gameObject.layer) != 0 && !affectedObjectDictionary.ContainsKey(other.gameObject)) {	
			HazardStatus(other.gameObject);

            IEnumerator hazardWaitCoroutine = HazardWaitCoroutine(other.gameObject);

			affectedObjectDictionary.Add(other.gameObject, hazardWaitCoroutine);

			StartCoroutine(hazardWaitCoroutine);
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

	private IEnumerator HazardWaitCoroutine(GameObject gameObject){
		yield return new WaitForSecondsRealtime(recoverTime);
		affectedObjectDictionary.Remove(gameObject);
	}
}
