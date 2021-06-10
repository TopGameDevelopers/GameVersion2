using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public GameObject[] frames;

    private int _counter = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
            GetNextFrame();
        if (Input.GetKeyDown(KeyCode.Escape))
            StartGame();
    }

    void GetNextFrame()
    {        
        if (_counter == frames.Length - 1 && SceneManager.GetActiveScene().buildIndex == 7)
        {
            StartGame();
            return;
        }

        if (_counter == frames.Length - 1 && SceneManager.GetActiveScene().buildIndex == 6)
        {
            SceneManager.LoadScene(0);
            return;
        }
        
        frames[_counter].gameObject.SetActive(false);
        _counter++;
        frames[_counter].gameObject.SetActive(true);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
