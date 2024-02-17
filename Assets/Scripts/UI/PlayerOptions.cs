using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour {
    
    [SerializeField] private bool isGamePaused;
    public void GetPlayer(CharacterSO characterSO) {
        Debug.Log(characterSO);
    }

    public void TogglePauseMenu() {
        if(isGamePaused) {
            //Run the unpause stuff
        } else {
            //Run the pause game stuff
        }
    }
}
