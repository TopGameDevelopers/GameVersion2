using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateLevels : MonoBehaviour
{
    public UnityEngine.UI.Button moonLevelButton;

    public Image[] marsStars;
    public Image[] enceladStars;
    public Image[] earthStars;
    public Image[] moonStars;

    public GameObject emptyImage;
    
    void Start()
    {
        var playerData = SaveSystem.LoadProgress();
        if (playerData is null)
        {
            moonLevelButton.interactable = false;
            emptyImage.SetActive(true);
            return;
        }

        var progressInfo = playerData.ProgressInformation;
        var starsAmount = 0;
        if (progressInfo.Count < 3)
        {
            moonLevelButton.interactable = false;
            emptyImage.SetActive(true);
        }

        var stars = CreateStars();
        foreach (var levelInfo in progressInfo)
        {
            for (var i = 0; i < levelInfo.Value; i++)
            {
                stars[levelInfo.Key][i].enabled = true;
                starsAmount++;
            }
        }

        if (starsAmount < 6)
        {
            moonLevelButton.interactable = false;
            emptyImage.SetActive(true);
        }
    }

    private Dictionary<int, Image[]> CreateStars()
    {
        var stars = new Dictionary<int, Image[]>
        {
            [1] = marsStars, [2] = enceladStars, [3] = earthStars, [4] = moonStars
        };

        return stars;
    }
}
