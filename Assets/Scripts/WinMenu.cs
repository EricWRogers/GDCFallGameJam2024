using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SuperPupSystems.Helper;

public class WinMenu : MonoBehaviour
{
    public GameObject winSection;

    public GameObject healthBar;
    public GameObject bulletArea;

    void Start()
    {
        winSection.SetActive(false);
    }

    public void Win()
    {
        Debug.Log("You have WON!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        winSection.SetActive(true);
        healthBar.SetActive(false);
        bulletArea.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void Retry()
    {
        //SceneManager.LoadSceneAysnc(1);
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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
