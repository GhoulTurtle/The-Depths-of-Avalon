using UnityEngine;

public class PlayerDamageTypeTesting : MonoBehaviour
{
    public DamageTypeSO damageTypeSO;
	public StatusEffect statusEffect;
	public float damageAmount;
	public HealthSystem healthSystem;
	private void Awake(){
		TryGetComponent(out healthSystem);
	}

	    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)) {
			damageTypeSO.DealDamage(healthSystem, damageAmount, statusEffect, transform);
        }
    }
}
