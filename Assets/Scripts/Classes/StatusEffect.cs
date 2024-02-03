using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
public class StatusEffect {
	[MinMaxRange(0.1f, 30f)]
	public RangedFloat statusDuration;
	public float statusStrength;
	public Status Status {get; private set;}

	private WaitForSecondsRealtime statusTimer = new WaitForSecondsRealtime(1f);
	
	public void SetStatus(Status _status){
		Status = _status;
	}

	public StatusEffect(RangedFloat _statusDuration, float _statusStrength, Status _status){
		statusDuration = _statusDuration;
		statusStrength = _statusStrength;
		Status = _status; 
	}

	public IEnumerator StatusEffectCoroutine(Character character){
		ChooseRandomStatusTimer();
		yield return statusTimer;
		character.FinishedEffect(this);
	}

	private void ChooseRandomStatusTimer(){
		var randomDuration = Random.Range(statusDuration.minValue, statusDuration.maxValue);
		statusTimer.waitTime = randomDuration;
	}
}
