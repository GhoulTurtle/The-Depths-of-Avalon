using System;

[Serializable]
public class StatusEffect {
	[MinMaxRange(0.1f, 30f)]
	public RangedFloat statusDuration;
	public float statusStrength;
	public Status Status {get; private set;}
	
	public void SetStatus(Status _status){
		Status = _status;
	}

	public StatusEffect(RangedFloat _statusDuration){
		statusDuration = _statusDuration;
	}
}
