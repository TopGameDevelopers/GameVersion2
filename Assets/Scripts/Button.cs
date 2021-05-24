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
        _audio = GetComponent<AudioSource>();
    }

    private void OpenDoor()
    {
        Instantiate(PressedButton, transform.position, transform.rotation);
        OpenedDoor.SetActive(true);
        ClosedDoor.SetActive(false);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _audio.Play();
       OpenDoor();
    }
}
