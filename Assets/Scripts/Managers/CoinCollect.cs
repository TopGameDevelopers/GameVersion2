using System;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect:MonoBehaviour
{
    public static int coinCount;
    private Text coinCounter;

    private void Start()
    {
        coinCounter = GetComponent<Text>();
        coinCount = 0;
    }

    private void Update()
    {
        coinCounter.text = $"✘{coinCount}";
    }
}