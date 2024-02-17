using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOptions : MonoBehaviour {
    
    [SerializeField] private bool isGamePaused;
    public void GetPlayer(Character character) {
        Debug.Log(character);
    }

    public void TogglePauseMenu() {
        if(isGamePaused) {
            Time.timeScale = 1.0f;
            Debug.Log("Game is no longer paused");
        } else {
            //Run the pause game stuff
        }
    }
}
