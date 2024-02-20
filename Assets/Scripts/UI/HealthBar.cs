//Last Editor: Caleb Richardson
//Last Edited: Feb 17

using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public HealthSystem playerHealth;
    public Slider HealthSlider;

    private PlayerUI playerUI;
    
    private void Awake() {
        playerUI = GetComponentInParent<PlayerUI>();
    }

    void Start() {
        playerUI.OnSetupUI += GetPlayer;
    }

    private void OnDestroy() {
        playerUI.OnSetupUI -= GetPlayer;
        if(playerHealth != null){
            playerHealth.OnDamaged -= SetCurrentHealth;
            playerHealth.OnHealed -= HealHealth;
            playerHealth.OnRespawn -= ResetHealth;
        }
    }

    private void GetPlayer(object sender, PlayerUI.SetupUIEventArgs e) {
        playerHealth = e.player.playerCharacter.GetComponent<HealthSystem>();
        playerHealth.OnDamaged += SetCurrentHealth;
        playerHealth.OnHealed += HealHealth;
        playerHealth.OnRespawn += ResetHealth;
        SetMaxHealth();
    }

    private void ResetHealth(object sender, EventArgs e){
        HealthSlider.value = playerHealth.GetMaxHealth();
    }

    public void SetMaxHealth() {
        HealthSlider.maxValue = playerHealth.GetMaxHealth();
        HealthSlider.value = playerHealth.GetMaxHealth();
    }

    public void SetCurrentHealth(object sender, HealthSystem.DamagedEventArgs e) {
        HealthSlider.value = playerHealth.GetCurrentHealth();
    }

    public void HealHealth(object sender, EventArgs e) {
        HealthSlider.value = playerHealth.GetCurrentHealth();
    }
}
