using System;

[Serializable]
public class StatusEffect {
	public float statusDuration;
	public float statusStrength;
	public Status status;
	
	public StatusEffect(float _statusDuration){
		statusDuration = _statusDuration;
	}
}
