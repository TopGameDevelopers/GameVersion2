using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using UnityEngine;
using UnityEngine.Serialization;

public class Button : MonoBehaviour
{
    [FormerlySerializedAs("ClosedDoor")] public GameObject closedDoor;
    [FormerlySerializedAs("OpenedDoor")] public GameObject openedDoor;
    [FormerlySerializedAs("PressedButton")] public GameObject pressedButton;

    public void Start()
    {
        openedDoor.SetActive(false);
    }

    private void OpenDoor()
    {
        Instantiate(pressedButton, transform.position, transform.rotation);
        openedDoor.SetActive(true);
        closedDoor.SetActive(false);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
       OpenDoor();
    }
}
