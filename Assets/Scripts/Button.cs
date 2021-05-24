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
        Debug.Log(transform.position);
        OpenedDoor.SetActive(true);
        ClosedDoor.SetActive(false);
        Destroy(gameObject);
        /*var x = transform.position.x;
        var y = transform.position.y;
        var z = -transform.position.z ;
        transform.SetPositionAndRotation(new Vector3(x,y,z), transform.rotation);*/
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        _audio.Play();
       OpenDoor();
    }
    
    // public void OnTriggerExit2D(Collider2D other)
    // {
    //     OpenDoor(false);
    // }
}
