using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class LoseMenu : MonoBehaviour
{
    public GameObject loseSection;

    public GameObject healthBar;
    public GameObject bulletArea;

    void Start()
    {
        loseSection.SetActive(false);
    }

    public void Lose()
    {
        Debug.Log("You have Lost!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseSection.SetActive(true);
        healthBar.SetActive(false);
        bulletArea.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu(string sceneName)
    {
       Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if (UNITY_EDITOR)
        Debug.Log("Quiting Play Mode");
        EditorApplication.ExitPlaymode();
#else
        Debug.Log("Quitting Build");
        Application.Quit();
#endif
    }
}
