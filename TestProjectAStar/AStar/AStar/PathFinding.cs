using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AStar
{
    public class PathFinding
    {
        private const int StraightMoveCost = 10;
        private const int DiagonalMoveCost = 14;

        private readonly Point _grid;
        private PathNode[] _pathNodes;

        private readonly Point _startPosition;
        private readonly Point _endPosition;

        private readonly Point _offsets;

        private readonly Point[] _obstacles;

        public PathFinding(int width, int height, Point startPosition, Point endPosition, Point offsets, Point[] obstacles)
        {
            _grid = new Point(width, height);
            _pathNodes = new PathNode[_grid.X * _grid.Y];
            _startPosition = startPosition;
            _endPosition = endPosition;

            _offsets = offsets;
            _obstacles = obstacles;
        }

        public List<Point> FindPath()
        {
            InitializePathNodes();
        
            var endNodeIndex = CalculateIndex(_endPosition.X, _endPosition.Y, _grid.X);

            var startNode = _pathNodes[CalculateIndex(_startPosition.X, _startPosition.Y, _grid.X)];
            startNode.GCost = 0;
            _pathNodes[startNode.Index] = startNode;

            var openList = new HashSet<int>();
            var closedList = new HashSet<int>();
        
            openList.Add(startNode.Index);

            while (openList.Count > 0)
            {
                var currentNode = _pathNodes[GetLowestFCostNodeIndex(openList, _pathNodes)];
                if (currentNode.Index == endNodeIndex)
                    break;

                openList.Remove(currentNode.Index);
                closedList.Add(currentNode.Index);

                AddNeighbours(openList, closedList, currentNode);
            }

            var endNode = _pathNodes[endNodeIndex];
            if (endNode.PreviousNodeIndex == -1)
                return null;

            var path = CalculatePath(_pathNodes, endNode);
            // foreach (var pathPosition in path) 
            //     Debug.Log(pathPosition);
            return path;
        }

        private void AddNeighbours(HashSet<int> openList, HashSet<int> closedList, PathNode currentNode)
        {
            var neighbours = GetNeighbours(currentNode, _grid);
            foreach (var neighbour in neighbours)
            {
                var neighbourNodeIndex = CalculateIndex(neighbour.X, neighbour.Y, _grid.X);
                if (closedList.Contains(neighbourNodeIndex))
                    continue;

                var neighbourNode = _pathNodes[neighbourNodeIndex];
                /*if (!neighbourNode.IsWalkable)
                    continue;*/

                var currentNodePosition = new Point(currentNode.X, currentNode.Y);
                var tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNodePosition, neighbour);
                if (tentativeGCost < neighbourNode.GCost)
                {
                    neighbourNode.PreviousNodeIndex = currentNode.Index;
                    neighbourNode.GCost = tentativeGCost;
                    _pathNodes[neighbourNodeIndex] = neighbourNode;

                    if (!openList.Contains(neighbourNode.Index)) 
                        openList.Add(neighbourNode.Index);
                }
            }
        }

        private void InitializePathNodes()
        {
            for (var x = 0; x < _grid.X; x++)
            for (var y = 0; y < _grid.Y; y++)
            {
                var pathNode = new PathNode
                {
                    X = x,
                    Y = y,
                    Index = CalculateIndex(x, y, _grid.X),
                    GCost = int.MaxValue,
                    HCost = CalculateDistanceCost(new Point(x, y), _endPosition),
                    //IsWalkable = true,
                    PreviousNodeIndex = -1
                };

                _pathNodes[pathNode.Index] = pathNode;
            }
        }

        private List<Point> CalculatePath(PathNode[] pathNodes, PathNode endNode)
        {
            var path = new List<Point> {new Point(endNode.X, endNode.Y)};
            var currentNode = endNode;
            while (currentNode.PreviousNodeIndex != -1)
            {
                var previousNode = pathNodes[currentNode.PreviousNodeIndex];
                path.Add(new Point(previousNode.X, previousNode.Y));
                currentNode = previousNode;
            }
            path.Reverse();

            return path;
        }

        private List<Point> GetNeighbours(PathNode currentNode, Point gridSize)
        {
            var neighbours = new List<Point>();
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                var possibleNeighbour = new Point(currentNode.X + dx, currentNode.Y + dy);
                if (IsInsideGrid(possibleNeighbour, gridSize) && IsEmpty(possibleNeighbour)) 
                    neighbours.Add(possibleNeighbour);
            }

            return neighbours;
        }

        private bool IsEmpty(Point gridPosition)
        {
            return !_obstacles.Contains(new Point(gridPosition.X - _offsets.X, gridPosition.Y - _offsets.Y));
            // return Physics2D.OverlapPoint(new Vector2(gridPosition.x - _offsets.x,
            //     gridPosition.y - _offsets.y)) is null;
        }

        private bool IsInsideGrid(Point gridPosition, Point gridSize)
        {
            return gridPosition.X >= 0 &&
                   gridPosition.Y >= 0 &&
                   gridPosition.X < gridSize.X &&
                   gridPosition.Y < gridSize.Y;
        }

        private int GetLowestFCostNodeIndex(HashSet<int> openList, PathNode[] pathNodes)
        {
            return pathNodes[openList.OrderBy(t => pathNodes[t].FCost).FirstOrDefault()].Index;
            /*var lowestFCostPathNode = pathNodes[openList[0]];
            foreach (var node in openList.Where(t => pathNodes[t].FCost < lowestFCostPathNode.FCost))
                lowestFCostPathNode = pathNodes[node];

            return lowestFCostPathNode.Index;*/
        }

        private int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;

        private int CalculateDistanceCost(Point start, Point end)
        {
            var xDistance = Math.Abs(start.X - end.X);
            var yDistance = Math.Abs(start.Y - end.Y);
            var remaining = Math.Abs(xDistance - yDistance);
            return DiagonalMoveCost * Math.Min(xDistance, yDistance) + StraightMoveCost * remaining;
        }
    
        private struct PathNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Index { get; set; }
            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost => GCost + HCost;
            //public bool IsWalkable { get; set; }
            public int PreviousNodeIndex { get; set; }
        }
    }
}