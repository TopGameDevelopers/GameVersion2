using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public float Speed = 5f;
    public Rigidbody2D rigitbody;

    public void Start()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        var xCoord = transform.position.x >= player.transform.position.x ? -1 : 1;
        var yCoord = transform.position.y >= player.transform.position.y ? -1 : 1;
        rigitbody.velocity = new Vector2(xCoord, yCoord) * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(player);
        }
    }
}
