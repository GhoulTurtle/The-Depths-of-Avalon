//Last Editor: Caleb Husselman
//Last Edited: Feb 17

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectMenu;

    private void Start() {
        levelSelectMenu.SetActive(false);
    }
    public void PlayPressed()
    {
        levelSelectMenu.SetActive(true);

        // Disabling this for the level select for our build
        // SceneManager.LoadScene(1);

    }


    public void QuitPressed()
    {
        Application.Quit();
    }

}
