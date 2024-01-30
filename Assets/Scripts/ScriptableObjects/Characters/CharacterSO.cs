using UnityEngine;

[CreateAssetMenu(menuName = "Character/CharacterSO", fileName = "NewCharacterSO")]
public class CharacterSO : ScriptableObject{
	public string CharacterName;
	public CharacterStatsSO CharacterStats;
	public CharacterVisualsSO CharacterVisuals;
	public CharacterAudioSO CharacterAudio;
}
