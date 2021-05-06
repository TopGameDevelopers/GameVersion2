using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Searcher
{
    private readonly GameObject _monster;

    private readonly GameObject _target;
    private readonly int _gridSize;

    private int2 offsets;
    
    public Searcher(GameObject target, GameObject monster, int gridSize)
    {
        _target = target;
        _monster = monster;
        _gridSize = gridSize;
    }

    private int2 GetTargetGridCoords()
    {
        var targetActualCoord = GetTargetActualCoords();
        return new int2(targetActualCoord.x - offsets.x, targetActualCoord.y - offsets.y);
    }

    private int2 GetMonsterActualCoords()
    {
        var monsterPosition = _monster.transform.position;
        var xActual = (int) monsterPosition.x;
        var yActual = (int) monsterPosition.y;
        return new int2(xActual, yActual);
    }

    private int2 GetTargetActualCoords()
    {
        var position = _target.transform.position;
        var xCoord = (int)position.x;
        var yCoord = (int)position.y;
        return new int2(xCoord, yCoord);
    }

    private int2 GetMonsterGridCoords()
    {
        return new int2(_gridSize / 2 + 1, _gridSize / 2 + 1);
    }

    private void FindOffset()
    {
        var monsterActualCoords = GetMonsterActualCoords();
        var monsterGridCoords = GetMonsterGridCoords();
        var xOffset = monsterActualCoords.x - monsterGridCoords.x;
        var yOffset = monsterActualCoords.y - monsterGridCoords.y;
        offsets = new int2(xOffset, yOffset);
    }

    private bool CanGoToTarget()
    {
        var targetActualCoords = GetTargetActualCoords();
        var monsterActualCoord = GetMonsterActualCoords();
        return math.abs(targetActualCoords.x - monsterActualCoord.x) <= _gridSize && 
               math.abs(targetActualCoords.y - monsterActualCoord.y) <= _gridSize;
    }

    public List<int2> GetPathAStar()
    {
        return CanGoToTarget()
            ? new PathFinding(_gridSize, _gridSize,
                    GetMonsterGridCoords(), GetTargetGridCoords())
                .FindPath()
                .Select(point => new int2(point.x + offsets.x, point.y + offsets.y))
                .ToList()
            : null;
    }
    /*public static SinglyLinkedList<Vector2> BreadthFindSearching(Vector2 playerPosition, Vector2 monsterPosition)
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
                    nextPoint, 0.5f, layerWall);#1#
                Collider2D[] results = new Collider2D[] { };
                var size = Physics2D.OverlapPointNonAlloc(currentPoint, results, layerWall);
                if (size == 0)
                {
                    queue.Enqueue(new SinglyLinkedList<Vector2>(nextPoint, currentPath));
                    visited.Add(nextPoint);
                }
            }
        }
    }*/
}
