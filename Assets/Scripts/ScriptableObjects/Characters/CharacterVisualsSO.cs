using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Visuals", fileName = "NewCharacterVisualsSO")]
public class CharacterVisualsSO : ScriptableObject{	
	[Header("Character References")]
	[Tooltip("The base model that the character spawns with, must include a animator component if wishing for model to be animated.")]
	public GameObject CharacterModelPrefab; 
	[Tooltip("The character sprite that would be used in UI.")]
	public Sprite CharacterSprite;
}
