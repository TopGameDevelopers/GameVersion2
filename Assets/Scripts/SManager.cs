using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame(GameObject main)
    {
        Camera.allCameras.First(x => x.name == "BeginCam").gameObject.SetActive(false);
        //Debug.Log(Camera.allCameras.ToString());
        main.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
