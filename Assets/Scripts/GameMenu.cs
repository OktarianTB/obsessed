using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{

    bool gameIsPaused;

    [SerializeField] GameObject menu;
    GameObject currentMenu;

    void Start()
    {
        gameIsPaused = false;
        menu.SetActive(gameIsPaused);
        Time.timeScale = 1f;

        if (!menu)
        {
            Debug.LogWarning("Menu object is missing");
        }
    }
    
    public void ManageMenu()
    {
        gameIsPaused = !gameIsPaused;

        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            menu.SetActive(gameIsPaused);
        }
        else
        {
            Time.timeScale = 1f;
            menu.SetActive(gameIsPaused);
        }
    }

}
