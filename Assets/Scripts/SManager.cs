using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    public void GoToNextLevel()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
}
