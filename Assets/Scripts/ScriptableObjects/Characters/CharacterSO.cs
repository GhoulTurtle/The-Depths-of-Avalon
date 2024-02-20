//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterSO", fileName = "NewCharacterSO")]
public class CharacterSO : ScriptableObject{
	[Header("Required Character References")]
	public string CharacterName;
	public CharacterStatsSO CharacterStats;
	public List<AbilitySO> CharacterAbilities;
	public CharacterVisualsSO CharacterVisuals;
	public CharacterAudioSO CharacterAudio;

	[Header("Shared Asset Reference")]
	public CharacterSharedAssetsSO SharedAssetsSO;
}
