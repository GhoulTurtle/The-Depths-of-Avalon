//Last Editor: Caleb Husselman
//Last Edited: Feb 16

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private bool isPlayerOne;

    private Caster caster;

    public event EventHandler<SetupUIEventArgs> OnSetupUI;
    public List<AbilityRecharge> abilityRecharges = new List<AbilityRecharge>();

    public class SetupUIEventArgs : EventArgs {
        public Player player;
        public SetupUIEventArgs(Player _player) {
            player = _player;
        }
    }

    private void Start() {
        PlayerManager.Instance.OnCorrectPlayerCount += StartUISetup;
    }

    private void OnDestroy() {
        PlayerManager.Instance.OnCorrectPlayerCount -= StartUISetup;
        if(caster != null) {
            caster.OnAbilityFired -= StartAbilityCooldown;
        }
    }

    private void StartUISetup(object sender, EventArgs e) {
        var playerNumber = isPlayerOne ? 0 : 1;
        Player player = PlayerManager.Instance.CurrentPlayerList[playerNumber];
        OnSetupUI?.Invoke(this, new SetupUIEventArgs(player));

        abilityRecharges.Clear(); //Clean out any old abilities

        AbilityRecharge[] abilityRechargeObjects = GetComponentsInChildren<AbilityRecharge>();
        foreach(AbilityRecharge ability in abilityRechargeObjects) {
            abilityRecharges.Add(ability);
        }

        caster = player.playerCharacter.gameObject.GetComponent<Caster>();
        if(caster != null) {
            caster.OnAbilityFired += StartAbilityCooldown;
        } else {
            Debug.LogError("caster cannot be found");
        }
    }

    private void StartAbilityCooldown(object sender, Caster.AbilityEventArgs e) {
        abilityRecharges[e.abilityNumber].UseAbility();
    }
}
