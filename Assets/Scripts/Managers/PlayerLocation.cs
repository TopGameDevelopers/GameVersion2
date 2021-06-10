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
            var distanceVector = player.transform.position - gameFlags[i].position;
            var distanceVector2D = new Vector2(distanceVector.x, distanceVector.y);
            if (distanceVector2D.magnitude <= 5)
            {
                transform.position = minimapLocation[i].position;
                break;
            }
        }
    }
}
