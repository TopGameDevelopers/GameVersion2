using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public GameObject Gem;
    public bool IsFinish = false;
    public GameObject Menu;
    public GameObject CCollect;
    public GameObject Health;

    private void Start()
    {
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Gem.activeSelf)
        {
            IsFinish = true;
            GetFinishMenu();
        }
    }

    void GetFinishMenu()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        CCollect.gameObject.SetActive(false);
        Health.gameObject.SetActive(false);
    }
    
}
