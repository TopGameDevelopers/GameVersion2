using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject player;
    public float Speed;
    public Rigidbody2D rigitbody;

    public void Start()
    {
        rigitbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        var playerPosition = new Vector2((int) player.transform.position.x, (int) player.transform.position.y);
        var monsterPosition = new Vector2((int) transform.position.x, (int) transform.position.y);
        var path = BreadthFindSearching(playerPosition, monsterPosition);
        foreach (var vector in path)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                vector, 0.5f);
        }
        /*var xCoord = transform.position.x >= player.transform.position.x ? -1 : 1;
        var yCoord = transform.position.y >= player.transform.position.y ? -1 : 1;
        rigitbody.velocity = new Vector2(xCoord, yCoord) * Speed;*/
    }

    private SinglyLinkedList<Vector2> BreadthFindSearching(Vector2 playerPosition, Vector2 monsterPosition)
    {
        var visited = new HashSet<Vector2>();
        var layerWall = 1 << 10;
        var queue = new Queue<SinglyLinkedList<Vector2>>();
        queue.Enqueue(new SinglyLinkedList<Vector2>(monsterPosition));
        while (queue.Any())
        {
            var currentPath = queue.Dequeue();
            var currentPoint = currentPath.Value;
            
            if (currentPoint == playerPosition)
                return currentPath;
            
            var wallColliders = Physics.OverlapSphere(currentPoint, 0.5f , layerWall);
            if(wallColliders.Any()) continue;
            visited.Add(currentPoint);
            AddNeighbors(queue, visited, currentPoint, currentPath, layerWall);
        }

        return null;
    }

    private void AddNeighbors(Queue<SinglyLinkedList<Vector2>> queue, HashSet<Vector2> visited, 
        Vector2 currentPoint, SinglyLinkedList<Vector2> currentPath, int layerWall)
    {
        for (var dy = -1; dy <= 1; dy++)
        for (var dx = -1; dx <= 1; dx++)
        {
            if (dx != 0 ^ dy != 0)
            {
                var nextPoint = new Vector2(currentPoint.x + dx, currentPoint.y + dy);
                var nextPointWallColliders = Physics.OverlapSphere(
                    nextPoint, 0.5f, layerWall);
                if (nextPointWallColliders.Length == 0)
                {
                    queue.Enqueue(new SinglyLinkedList<Vector2>(nextPoint,currentPath));
                    visited.Add(nextPoint);
                }
            }
        }
    }
}
