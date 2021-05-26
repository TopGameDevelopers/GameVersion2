using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private static bool _gameIsPaused;
    public GameObject pauseMenuUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_gameIsPaused)
            {
                ContinueGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void ContinueGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void LoadMenu()
    {
        Debug.Log("load menu");
    }

    public void ShowSettings()
    {
        Debug.Log("show settings");
    }

    public void QuitGame()
    {
        Debug.Log("quit");
    }
}
