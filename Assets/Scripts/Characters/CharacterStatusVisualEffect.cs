using UnityEngine;

public class CharacterStatusVisualEffect : MonoBehaviour{
	[SerializeField] private Status statusAssociated;
	private bool deleteUponCompletion = true;
	private Character character;

	private ParticleSystem vfxParticleSystem;

	private float defaultDeleteTime = 10f;

	private void Awake() {
		TryGetComponent(out vfxParticleSystem);
		if(vfxParticleSystem == null){
			Debug.LogError("<color=red>No particle system found on: " + gameObject.name + " deleting visual effect in " + defaultDeleteTime + " seconds...</color>");
			Invoke(nameof(DeleteVisualEffect), defaultDeleteTime);
		}
	}

	private void Start() {
		if(vfxParticleSystem != null){
			var main = vfxParticleSystem.main;
			main.stopAction = ParticleSystemStopAction.Callback;
		}
	}

	private void OnParticleSystemStopped() {
		if(deleteUponCompletion) DeleteVisualEffect();	
	}

	public void SetupVisualEffect(Character _character){
		if(vfxParticleSystem != null){
			deleteUponCompletion = !vfxParticleSystem.main.loop;
		}

		character = _character;
		if(character != null){
			character.OnStatusEffectFinished += DeleteVisualEffect;
		} 

		vfxParticleSystem.Play();
	}

    private void DeleteVisualEffect(object sender, Character.StatusEffectAppliedEventArgs e){
        if(e.abilityEffect.Status == statusAssociated){
        	Destroy(gameObject);
		}
    }

    private void DeleteVisualEffect(){
		Destroy(gameObject);
	}

	private void OnDestroy() {
		if(character != null){
			character.OnStatusEffectFinished -= DeleteVisualEffect;
		}
	}
}
