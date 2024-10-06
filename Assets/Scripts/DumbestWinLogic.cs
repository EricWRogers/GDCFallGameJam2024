using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DumbestWinLogic : MonoBehaviour
{
    public GameObject win;
    void FixedUpdate()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            win.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
    }
    
}
