using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject ClosedDoor;

    public GameObject OpenedDoor;
    

    public void Start()
    {
        OpenedDoor.SetActive(false);
    }

    private void OpenDoor(bool isOpen)
    {
        OpenedDoor.SetActive(isOpen);
        ClosedDoor.SetActive(!isOpen);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
       OpenDoor(true);
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        OpenDoor(false);
    }
}
