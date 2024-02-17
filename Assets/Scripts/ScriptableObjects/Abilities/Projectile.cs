//Last Editor: Caleb Richardson
//Last Edited: Feb 15

using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour{
	[Header("Projectile References")]
	[SerializeField] private float projectileSpeed = 5f;
	[SerializeField] private float collisonDetectionRadius = 3f;
	[SerializeField] private float maxProjectileLiveTime = 30f;
	
	[Header("Required References")]
	[SerializeField] private LayerMask dealDamageLayers; 
	[SerializeField] private LayerMask destoryProjectileLayers;
	[SerializeField] private int maxTargetColliders;

	private float damageAmount;
	private DamageTypeSO damageTypeSO;
	private StatusEffect statusEffect;

	private Collider[] targetColliders;

	private Vector3 moveDir;

	private void Start() {
		targetColliders = new Collider[maxTargetColliders];
	}

	public void SetupProjectile(float _damageAmount, DamageTypeSO _damageTypeSO, StatusEffect _statusEffect, Vector3 _moveDir){
		damageAmount = _damageAmount;
		damageTypeSO = _damageTypeSO;
		statusEffect = _statusEffect;
		moveDir = _moveDir;

		StartCoroutine(ProjectileLiveTimerCoroutine());
	}

	private void Update() {
		MoveForward();
		DetectCollison();
	}

	private void OnDestroy() {
		StopAllCoroutines();
	}

	private void DetectCollison(){
		if(Physics.OverlapSphereNonAlloc(transform.position, collisonDetectionRadius, targetColliders, dealDamageLayers) != 0){
			foreach (Collider target in targetColliders){
				if(target == null || !target.TryGetComponent(out HealthSystem healthSystem)) continue;

				if(damageTypeSO != null){
					damageTypeSO.DealDamage(healthSystem, damageAmount, statusEffect, transform);
				}
				else{
					healthSystem.TakeDamage(damageTypeSO, damageAmount, transform);
				}
			}
		}

		if(Physics.CheckSphere(transform.position, collisonDetectionRadius, destoryProjectileLayers)){
			Destroy(gameObject);
		}
	}

	private void MoveForward(){
		transform.position += projectileSpeed * Time.deltaTime * moveDir;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, collisonDetectionRadius);	
	}

	private IEnumerator ProjectileLiveTimerCoroutine(){
		yield return new WaitForSecondsRealtime(maxProjectileLiveTime);
		Destroy(gameObject);
	}
}
