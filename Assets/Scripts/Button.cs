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

    public GameObject PressedButton;

    private AudioSource _audio;
    
    public void Start()
    {
        OpenedDoor.SetActive(false);
        PressedButton.SetActive(false);
        _audio = GetComponent<AudioSource>();
    }

    private void OpenDoor(bool isOpen)
    {
        PressedButton.SetActive(isOpen);
        OpenedDoor.SetActive(isOpen);
        ClosedDoor.SetActive(!isOpen);
        var x = transform.position.x;
        var y = transform.position.y;
        var z = -transform.position.z ;
        transform.SetPositionAndRotation(new Vector3(x,y,z), transform.rotation);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
       OpenDoor(true);
       _audio.Play();
    }
    
    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     OpenDoor(false);
    // }
}
