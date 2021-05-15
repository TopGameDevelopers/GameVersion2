using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishCoins : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    public static int count = 0;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"✘{count}";
    }
}
