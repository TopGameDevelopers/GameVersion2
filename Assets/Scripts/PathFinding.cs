/*
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public static class PathFinding
{
    public static List<Vector2> FindPath(Vector2 start, Vector2 end, Grid grid)
    {
        var startNode = new Node(start);
        var endNode = new Node(end);
        var visited = new HashSet<Node>();
        var openList = new List<Node>();

        for (var dx = -10; dx < 10; dx++)
        for (var dy = -10; dy < 10; dy++)
        {
            var node = new Node(new Vector2(startNode.position.x + dx, startNode.position.y + dy));
            openList.Add(node);
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Any())
        {
            var currentNode = GetLowestCostNode(openList);
            if (currentNode.position == endNode.position)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            visited.Add(currentNode);

            var neighbourNodes = GetNeighbourNodes(currentNode, visited);
            foreach (var neighbourNode in neighbourNodes)
            {
                if(visited.Contains(neighbourNode))
                    continue;
                var tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.previous = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        return null;
    }

    private static int CalculateDistanceCost(Node firstNode, Node secondNode)
    {
        var distance = firstNode.position - secondNode.position;
        var remaining = (int) Math.Abs(distance.x - distance.y);
        return 14 * Math.Min((int) distance.x, (int) distance.y) + 10 * remaining;
    }

    private static Node GetLowestCostNode(List<Node> nodeList)
    {
        return nodeList.OrderBy(node => node.fCost).FirstOrDefault();
    }

    private static List<Node> GetNeighbourNodes(Node currentNode, HashSet<Node> visited, Grid grid)
    {
        var neighbourNodes = new List<Node>();
        for (var dx = -1; dx <= 1; dx++)
        for (var dy = -1; dy <= 1; dy++)
        {
            var point = new Vector2((int)currentNode.position.x + dx, (int)currentNode.position.y + dy);
            var node = new Node(point);
            if (grid.gameObject.name == "Walls")
            {
                visited.Add(node);
                continue;
            }
            if (dx != 0 || dy != 0)
            {
                neighbourNodes.Add(node);
            }
        }

        return neighbourNodes;
    }

    private static List<Vector2> CalculatePath(Node endNode)
    {
        var path = new List<Vector2> {endNode.position};
        var currentNode = endNode;
        while (currentNode.previous != null)
        {
            path.Add(currentNode.previous.position);
            currentNode = currentNode.previous;
        }

        path.Reverse();
        return path;
    }
}
*/
