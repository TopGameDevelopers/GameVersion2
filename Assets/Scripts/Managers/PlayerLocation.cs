using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocation : MonoBehaviour
{
    public GameObject player;
    public Transform[] gameFlags;
    public Transform[] minimapLocation;

    private void Update()
    {
        for (var i = 0; i < gameFlags.Length; i++)
        {
            if ((player.transform.position - gameFlags[i].position).magnitude <= 15)
            {
                transform.position = minimapLocation[i].position;
                break;
            }
        }
    }
}
