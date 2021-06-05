using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnManager : MonoBehaviour
{
    public GameObject Plate;
    public GameObject Block;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Plate.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Plate);
            Block.gameObject.SetActive(true);
        }
    }
}
