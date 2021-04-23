using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Door;
    //public int timePeriodInSeconds = 1;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Door.SetActive(false);
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        Door.SetActive(true);
    }
}
