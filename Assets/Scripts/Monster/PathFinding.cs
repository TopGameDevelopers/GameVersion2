using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;

namespace Monster
{
    public class PathFinding
    {
        private const int StraightMoveCost = 10;
        private const int DiagonalMoveCost = 14;

        private readonly int2 _grid;
        private PathNode[] _pathNodes;
        private readonly int2 _startPosition;
        private readonly int2 _endPosition;
        private readonly int2 _offsets;
        private readonly int2[] _obstacles;

        public PathFinding(int width, int height, int2 startPosition, int2 endPosition, int2 offsets, int2[] obstacles)
        {
            _grid = new int2(width, height);
            _pathNodes = new PathNode[_grid.x * _grid.y];
            _startPosition = startPosition;
            _endPosition = endPosition;

            _offsets = offsets;
            _obstacles = obstacles;
        }

        public List<int2> FindPath()
        {
            InitializePathNodes();
            var endNodeIndex = CalculateIndex(_endPosition.x, _endPosition.y, _grid.x);
            var startNode = _pathNodes[CalculateIndex(_startPosition.x, _startPosition.y, _grid.x)];
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
            return path;
        }

        private void AddNeighbours(HashSet<int> openList, HashSet<int> closedList, PathNode currentNode)
        {
            var neighbours = GetNeighbours(currentNode, _grid);
            foreach (var neighbour in neighbours)
            {
                var neighbourNodeIndex = CalculateIndex(neighbour.x, neighbour.y, _grid.x);
                if (closedList.Contains(neighbourNodeIndex))
                    continue;
                var neighbourNode = _pathNodes[neighbourNodeIndex];
                var currentNodePosition = new int2(currentNode.X, currentNode.Y);
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
            for (var x = 0; x < _grid.x; x++)
            for (var y = 0; y < _grid.y; y++)
            {
                var pathNode = new PathNode
                {
                    X = x,
                    Y = y,
                    Index = CalculateIndex(x, y, _grid.x),
                    GCost = int.MaxValue,
                    HCost = CalculateDistanceCost(new int2(x, y), _endPosition),
                    PreviousNodeIndex = -1
                };

                _pathNodes[pathNode.Index] = pathNode;
            }
        }

        private List<int2> CalculatePath(PathNode[] pathNodes, PathNode endNode)
        {
            var path = new List<int2> {new int2(endNode.X, endNode.Y)};
            var currentNode = endNode;
            while (currentNode.PreviousNodeIndex != -1)
            {
                var previousNode = pathNodes[currentNode.PreviousNodeIndex];
                path.Add(new int2(previousNode.X, previousNode.Y));
                currentNode = previousNode;
            }
            path.Reverse();

            return path;
        }

        private List<int2> GetNeighbours(PathNode currentNode, int2 gridSize)
        {
            var neighbours = new List<int2>();
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                var possibleNeighbour = new int2(currentNode.X + dx, currentNode.Y + dy);
                if (IsInsideGrid(possibleNeighbour, gridSize) && IsEmpty(possibleNeighbour)) 
                    neighbours.Add(possibleNeighbour);
            }

            return neighbours;
        }

        private bool IsEmpty(int2 gridPosition) => 
            !_obstacles.Contains(new int2(gridPosition.x - _offsets.x, gridPosition.y - _offsets.y));

        private bool IsInsideGrid(int2 gridPosition, int2 gridSize)
        {
            return gridPosition.x >= 0 &&
                   gridPosition.y >= 0 &&
                   gridPosition.x < gridSize.x &&
                   gridPosition.y < gridSize.y;
        }

        private int GetLowestFCostNodeIndex(HashSet<int> openList, PathNode[] pathNodes)
        {
            return pathNodes[openList.OrderBy(t => pathNodes[t].FCost).FirstOrDefault()].Index;
        }

        private int CalculateIndex(int x, int y, int gridWidth) => x + y * gridWidth;

        private int CalculateDistanceCost(int2 start, int2 end)
        {
            var xDistance = math.abs(start.x - end.x);
            var yDistance = math.abs(start.y - end.y);
            var remaining = math.abs(xDistance - yDistance);
            return DiagonalMoveCost * math.min(xDistance, yDistance) + StraightMoveCost * remaining;
        }
    
        private struct PathNode
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Index { get; set; }
            public int GCost { get; set; }
            public int HCost { get; set; }
            public int FCost => GCost + HCost;
            public int PreviousNodeIndex { get; set; }
        }
    }
}