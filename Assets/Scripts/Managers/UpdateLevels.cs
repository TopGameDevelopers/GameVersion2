using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateLevels : MonoBehaviour
{
    public UnityEngine.UI.Button moonLevelButton;
    void Start()
    {
        var playerData = SaveSystem.LoadProgress();
        if (playerData is null || playerData.LevelsCompleted.Count < 3)
        {
            moonLevelButton.interactable = false;
        }
    }
}
