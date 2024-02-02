using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayPressed()
    {
       SceneManager.LoadScene("Richardson Dev Scene");

    }


    public void QuitPressed()
    {
        Application.Quit();
    }

}
