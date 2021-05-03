using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.VFX;

public class PathFinding : MonoBehaviour
{
    private int straightMoveCost = 10;
    private int diagonalMoveCost = 14;

    private void Start()
    {
        FindPath(new int2(0, 0), new int2(3, 1));
    }
    private void FindPath(int2 startPosition, int2 endPosition)
    {
        var gridSize = new int2(4, 4);

        NativeArray<PathNode> pathNodes = new NativeArray<PathNode>(gridSize.x * gridSize.y, Allocator.Temp);

        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                var pathNode = new PathNode();
                pathNode.x = x;
                pathNode.y = y;
                pathNode.index = CalculateIndex(x, y, gridSize.x);

                pathNode.gCost = int.MaxValue;
                pathNode.hCost = CalculateDistanceCost(new int2(x, y), endPosition);
                pathNode.CalculateFCost();

                pathNode.isWalkable = true;
                pathNode.previousNodeIndex = -1;

                pathNodes[pathNode.index] = pathNode;
            }
        }

        var endNodeIndex = CalculateIndex(endPosition.x, endPosition.y, gridSize.x);

        PathNode startNode = pathNodes[CalculateIndex(startPosition.x, startPosition.y, gridSize.x)];
        startNode.gCost = 0;
        startNode.CalculateFCost();
        pathNodes[startNode.index] = startNode;

        var openList = new List<int>();
        var closedList = new List<int>();
        
        openList.Add(startNode.index);

        while (openList.Count >0)
        {
            var currentNodeIndex = GetLowestFCostNodeIndex(openList, pathNodes);
            var currentNode = pathNodes[currentNodeIndex];

            if (currentNodeIndex == endNodeIndex)
            {
                break;
            }
            
            for(var i=0;i<openList.Count;i++)
            {
                if (openList[i] == currentNodeIndex)
                {
                    openList.RemoveAt(i);
                    break;
                }
            }
            
            closedList.Add(currentNodeIndex);

            var neighbours = GetNeighbours(currentNode, gridSize);
            foreach (var neighbour in neighbours)
            {
                var neighbourNodeIndex = CalculateIndex(neighbour.x, neighbour.y, gridSize.x);
                if (closedList.Contains(neighbourNodeIndex))
                {
                    continue;
                }

                var neighbourNode = pathNodes[neighbourNodeIndex];
                if (!neighbourNode.isWalkable)
                {
                    continue;
                }

                var currentNodePosition = new int2(currentNode.x, currentNode.y);
                var tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNodePosition, neighbour);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNodeIndex = currentNodeIndex;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.CalculateFCost();
                    pathNodes[neighbourNodeIndex] = neighbourNode;

                    if (!openList.Contains(neighbourNode.index))
                    {
                        openList.Add(neighbourNode.index);
                    }
                }
            }
        }

        var endNode = pathNodes[endNodeIndex];
        if (endNode.previousNodeIndex == -1)
        {
            Debug.Log("Didn't find a path!");
        }
        else
        {
            var path = CalculatePath(pathNodes, endNode);

            foreach (var pathPosition in path)
            {
                Debug.Log(pathPosition);
            }
        }
        
        pathNodes.Dispose();
    }

    private List<int2> CalculatePath(NativeArray<PathNode> pathNodes, PathNode endNode)
    {
        var path = new List<int2> {new int2(endNode.x, endNode.y)};
        var currentNode = endNode;
        while (currentNode.previousNodeIndex != -1)
        {
            var previousNode = pathNodes[currentNode.previousNodeIndex];
            path.Add(new int2(previousNode.x, previousNode.y));
            currentNode = previousNode;
        }

        return path;
    }

    private List<int2> GetNeighbours(PathNode currentNode, int2 gridSize)
    {
        var neighbours = new List<int2>();
        for (var dx = -1; dx <= 1; dx++)
        {
            for (var dy = -1; dy <= 1; dy++)
            {
                var possibleNeighbour = new int2(currentNode.x + dx, currentNode.y + dy);
                if (IsInsideGrid(possibleNeighbour, gridSize))
                {
                    neighbours.Add(possibleNeighbour);
                }
            }
        }

        return neighbours;
    }

    private bool IsInsideGrid(int2 gridPosition, int2 gridSize)
    {
        return gridPosition.x >= 0 &&
               gridPosition.y >= 0 &&
               gridPosition.x < gridSize.x &&
               gridPosition.y < gridSize.y;
    }

    private int GetLowestFCostNodeIndex(List<int> openList, NativeArray<PathNode> pathNodes)
    {
        var lowestFCostPathNode = pathNodes[openList[0]];
        foreach (var node in openList.Where(t => pathNodes[t].fCost < lowestFCostPathNode.fCost))
        {
            lowestFCostPathNode = pathNodes[node];
        }

        return lowestFCostPathNode.index;
    }

    private int CalculateIndex(int x, int y, int gridWidth)
    {
        return x + y * gridWidth;
    }


    private int CalculateDistanceCost(int2 start, int2 end)
    {
        var xDistance = math.abs(start.x - end.x);
        var yDistance = math.abs(start.y - end.y);
        var remaining = math.abs(xDistance - yDistance);
        return diagonalMoveCost * math.min(xDistance, yDistance) + straightMoveCost * remaining;
    }
    private struct PathNode
    {
        public int x;
        public int y;

        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;

        public int previousNodeIndex;

        public void CalculateFCost()
        {
            fCost = gCost + hCost;
        }
    }
}
