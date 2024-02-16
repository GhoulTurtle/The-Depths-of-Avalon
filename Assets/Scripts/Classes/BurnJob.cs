//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System.Collections;
using UnityEngine;

public class BurnJob{
	public float DamagePerTick = 10f;
	public float TicksPerSecond = 0.6f;
	public WaitForSecondsRealtime TickTime;
	private HealthSystem parentHealthSystem;

	private bool isBurning;

	public BurnJob(HealthSystem _parentHealthSystem){
		parentHealthSystem = _parentHealthSystem;
		TickTime = new WaitForSecondsRealtime(TicksPerSecond);
	}

	public IEnumerator BurnDOTCorutine(){
		isBurning = true;
		while(isBurning){
			parentHealthSystem.TakeDamage(null, DamagePerTick, null);
			Debug.Log("Burned " + parentHealthSystem.name + " for: " + DamagePerTick);
			yield return TickTime;
		}
	}

	public void FinishedBurning(){
		isBurning = false;
	}
}
