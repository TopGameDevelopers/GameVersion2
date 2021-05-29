using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLevels : MonoBehaviour
{
    void Start()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LevelComplete", sceneIndex - 1);
    }
}
