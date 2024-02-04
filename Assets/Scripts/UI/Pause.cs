using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{

    public bool isPaused = false;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
       Time.timeScale = 1;
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if(!isPaused)
            {
                GamePaused();
                isPaused = true;
            }
        }
    }

    void GamePaused()
    {
      Time.timeScale = 0;
      pauseMenu.SetActive(true); 
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
    
    public void GameUnPaused()
    {
      Time.timeScale = 1;
      pauseMenu.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      isPaused = false;
    }
    public void MainMenu()
    {
      SceneManager.LoadScene("Main Menu");
    }
    
public void Quit()
{
Application.Quit();

}


}
