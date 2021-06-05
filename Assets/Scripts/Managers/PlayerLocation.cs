using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLocation : MonoBehaviour
{
    public GameObject player;
    public Transform leftCorner;
    public Transform rightCorner;

    public Image map;
    public float mapImageWidth;
    public float mapImageHeight;

    private void Update()
    {
        var offsets = (Math.Abs(leftCorner.position.x - player.transform.position.x), 
            Math.Abs(leftCorner.position.y - player.transform.position.y));
        var levelMapWidth = rightCorner.position.x - leftCorner.position.x;
        var levelMapHeight = rightCorner.position.y - leftCorner.position.y;

        var (mapImageLeftCornerX, mapImageLeftCornerY) = (map.transform.position.x - mapImageWidth / 2,
            map.transform.position.y - mapImageHeight / 2);

        transform.position = new Vector2(mapImageLeftCornerX + offsets.Item1 * mapImageWidth / levelMapWidth,
            mapImageLeftCornerY + offsets.Item2 * mapImageHeight / levelMapHeight);
    }
}
