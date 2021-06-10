using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "player.fun");
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        SceneManager.LoadScene(7);
    }
}
