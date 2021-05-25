using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{

    public UnityEngine.UI.Button level2;

    public UnityEngine.UI.Button level3;

    public UnityEngine.UI.Button level4;
    // Start is called before the first frame update
    public int LevelComplete;
    void Start()
    {
        LevelComplete = PlayerPrefs.GetInt("LevelComplete");
        level2.interactable = false;
        level3.interactable = false;
        level4.interactable = false;

        switch (LevelComplete)
        {
            case 1:
                level2.interactable = true;
                break;
            case 2:
                level2.interactable = true;
                level3.interactable = true;
                break;
            case 3:
                level2.interactable = true;
                level3.interactable = true;
                level4.interactable = true;
                break;
        }
    }

    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
