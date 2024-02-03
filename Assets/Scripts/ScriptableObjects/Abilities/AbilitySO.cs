using UnityEngine;

public abstract class AbilitySO : ScriptableObject{
	[Header("Ability Lore")]
	public string AbilityName;

	[Header("Ability Stats")]
	[Tooltip("How long the ability takes to trigger, plays charging animation.")]
	public float CastTime;
	[Tooltip("How long the ability takes to recharge in seconds.")]
	public float Cooldown;
	[Tooltip("The damage type the ability will use, if applicable.")]
	public DamageTypeSO AbilityDamageType;
	[Tooltip("The range of damage the ability can do."), MinMaxRange(1f, 50f)]
	public RangedFloat AbilityDamageAmount;
	[Tooltip("Does this ability have cancel functionality?")]
	public bool IsCancelable = false;

	[Header("Ability Visuals")]
	public GameObject AbilityVisuals;
	
	[Header("Ability Audio")]
	public AudioEvent CastingAudio;
	public AudioEvent FiredAudio;
	public AudioEvent CanceledAudio;

	public abstract void CastAbility(Caster caster);
	public abstract void CancelAbility(Caster caster);
}
