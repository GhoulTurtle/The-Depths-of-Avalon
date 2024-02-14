using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    PlayerUI playerUI => GetComponentInParent<PlayerUI>();
    public HealthSystem playerHealth;
    public Slider HealthSlider;

    void Start() {
        playerUI.OnSetupUI += GetPlayer;
    }

    private void OnDestroy() {
        playerUI.OnSetupUI -= GetPlayer;
        playerHealth.OnDamaged -= SetCurrentHealth;
        playerHealth.OnHealed -= HealHealth;
    }

    private void GetPlayer(object sender, PlayerUI.SetupUIEventArgs e) {
        playerHealth = e.player.playerCharacter.GetComponent<HealthSystem>();
        playerHealth.OnDamaged += SetCurrentHealth;
        playerHealth.OnHealed += HealHealth;
        SetMaxHealth();
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
