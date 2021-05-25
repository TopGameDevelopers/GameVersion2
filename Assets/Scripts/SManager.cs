using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    public static LevelController instance = null;

    private int sceneIndex;
    public int levelComplete;
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void IsEndGame()
    {
        if (sceneIndex == 5)
        {
            Invoke("LoadMainMenu", 1f);
        }
        else
        {
            if (levelComplete < sceneIndex)
                PlayerPrefs.SetInt("LevelComplete", sceneIndex + 1);
            Invoke("NextLevel", 1f);
        }
    }
}
