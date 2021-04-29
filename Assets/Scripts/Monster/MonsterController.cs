using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    [FormerlySerializedAs("Speed")] public float speed;
    public Rigidbody2D rigitbody;
    public int fieldOfView;
    

    public void Start()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        var playerPosition = new Vector2((int) player.transform.position.x, (int) player.transform.position.y);
        var monsterPosition = new Vector2((int) transform.position.x, (int) transform.position.y);
        if ((playerPosition - monsterPosition).magnitude <= fieldOfView)
        {
            var path = Searcher.BreadthFindSearching(playerPosition, monsterPosition);
            foreach (var vector in path)
            {
                var step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, vector, step);
            }
        }

        /*var xCoord = transform.position.x >= player.transform.position.x ? -1 : 1;
        var yCoord = transform.position.y >= player.transform.position.y ? -1 : 1;
        rigitbody.velocity = new Vector2(xCoord, yCoord) * Speed;*/
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}