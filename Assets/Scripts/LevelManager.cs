using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    int currentBuildIndex; // 0: menu, 1: level, 2: end screen
    GameMenu gameMenu;

    void Start()
    {
        gameMenu = FindObjectOfType<GameMenu>();
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    void Update()
    {
        ManageInput();
    }

    private void ManageInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentBuildIndex == 0 || currentBuildIndex == 2)
            {
                LoadGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentBuildIndex == 0)
            {
                LeaveGame();
            }
            else if(currentBuildIndex == 2)
            {
                LoadMenu();
            } 
            else if(currentBuildIndex == 1)
            {
                if (gameMenu)
                {
                    gameMenu.ManageMenu();
                }
            }
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadNextLevel()
    {
        if(currentBuildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentBuildIndex + 1);
        }
        else
        {
            LoadMenu();
        }
    }

    public void LeaveGame()
    {
        Application.Quit();
    }

    public void ContinuePlaying()
    {
        gameMenu.ManageMenu();
    }

}
