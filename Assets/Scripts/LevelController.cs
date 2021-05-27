using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    private int sceneIndex;
    public int LevelComplete;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LevelComplete = PlayerPrefs.GetInt("LevelComplete");
    }

    public void IsEndGame()
    {
        if (sceneIndex == 5)
        {
            Invoke("LoadMainMenu", 1f);
        }
        else
        {
            if (LevelComplete < sceneIndex)
                PlayerPrefs.SetInt("LevelComplete", sceneIndex - 1);
            Invoke("NextLevel", 1f);
        }
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
