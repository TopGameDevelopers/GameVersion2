using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LevelComplete", sceneIndex - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
