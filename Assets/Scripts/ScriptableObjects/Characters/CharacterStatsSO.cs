using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats", fileName = "NewCharacterStatsSO")]
public class CharacterStatsSO : ScriptableObject{
	[Header("Movement Stats")]
	public float movementSpeed;
	public float accelerationSpeed;
	public float rotationSpeed;

	[Header("Base Stats")]
	public float maxHealth;
}
