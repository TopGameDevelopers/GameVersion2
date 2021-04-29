using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Searcher
{
    public static SinglyLinkedList<Vector2> BreadthFindSearching(Vector2 playerPosition, Vector2 monsterPosition)
    {
        var visited = new HashSet<Vector2>();
        var mask = LayerMask.GetMask("Walls");
        var queue = new Queue<SinglyLinkedList<Vector2>>();
        queue.Enqueue(new SinglyLinkedList<Vector2>(monsterPosition));
        while (queue.Any())
        {
            var currentPath = queue.Dequeue();
            var currentPoint = currentPath.Value;

            if (currentPoint == playerPosition)
                return currentPath;

            visited.Add(currentPoint);
            AddNeighbors(queue, visited, currentPoint, currentPath, mask);
        }

        return null;
    }

    private static void AddNeighbors(Queue<SinglyLinkedList<Vector2>> queue, HashSet<Vector2> visited,
        Vector2 currentPoint, SinglyLinkedList<Vector2> currentPath, int layerWall)
    {
        for (var dy = -1; dy <= 1; dy++)
        for (var dx = -1; dx <= 1; dx++)
        {
            if (dx != 0 || dy != 0)
            {
                var nextPoint = new Vector2(currentPoint.x + dx, currentPoint.y + dy);
                /*var nextPointWallColliders = Physics.OverlapSphere(
                    nextPoint, 0.5f, layerWall);*/
                Collider2D[] results = new Collider2D[] { };
                var size = Physics2D.OverlapPointNonAlloc(currentPoint, results, layerWall);
                if (size == 0)
                {
                    queue.Enqueue(new SinglyLinkedList<Vector2>(nextPoint, currentPath));
                    visited.Add(nextPoint);
                }
            }
        }
    }
}
