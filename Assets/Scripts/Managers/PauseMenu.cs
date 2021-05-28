using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool _gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject pauseSettings;
    public GameObject backButton;

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

    public void ShowSettings()
    {
        pauseSettings.SetActive(true);
        backButton.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToSelectLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToPause()
    {
        pauseSettings.SetActive(false);
        backButton.SetActive(false);
    }
}