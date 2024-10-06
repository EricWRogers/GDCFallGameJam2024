using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject healthBar;
    public GameObject bulletArea;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        healthBar.SetActive(true);
        bulletArea.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    void Pause ()
    {
        pauseMenuUI.SetActive(true);
        healthBar.SetActive(false);
        bulletArea.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
       Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu UI");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }
}
