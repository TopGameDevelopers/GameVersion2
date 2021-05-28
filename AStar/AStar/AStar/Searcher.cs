using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace AStar
{
    public class Searcher
    {
        private readonly Vector2 position;
        private readonly Vector2 monsterPosition;
        private readonly int _gridSize;

        private readonly Point _offsets;

        private readonly Point[] _obstacles;
    
        public Searcher(Vector2 target, Vector2 monster, int gridSize, Point[] obstacles)
        {
            position = target;
            monsterPosition = monster;
            _gridSize = gridSize;
            _offsets = FindOffset();
            _obstacles = obstacles;
        }

        private Point GetMonsterActualCoords()
        {
            var xActual = (int) monsterPosition.X;
            var yActual = (int) monsterPosition.Y;
            return new Point(xActual, yActual);
        }

        private Point GetTargetActualCoords()
        {
            var xCoord = (int)position.X;
            var yCoord = (int)position.Y;
            return new Point(xCoord, yCoord);
        }
    
        private Point GetTargetGridCoords()
        {
            var targetActualCoord = GetTargetActualCoords();
            return new Point(targetActualCoord.X + _offsets.X, targetActualCoord.Y + _offsets.Y);
        }

        private Point GetMonsterGridCoords()
        {
            return new Point(_gridSize / 2, _gridSize / 2);
        }

        private Point FindOffset()
        {
            var monsterActualCoords = GetMonsterActualCoords();
            var monsterGridCoords = GetMonsterGridCoords();
            var xOffset = monsterGridCoords.X - monsterActualCoords.X;
            var yOffset = monsterGridCoords.Y - monsterActualCoords.Y;
            return new Point(xOffset, yOffset);
        }

        private bool CanGoToTarget()
        {
            var targetGridCoords = GetTargetGridCoords();
            return targetGridCoords.X >= 0 &&
                   targetGridCoords.X < _gridSize &&
                   targetGridCoords.Y >= 0 &&
                   targetGridCoords.Y < _gridSize;
        }

        public List<Point> GetPathAStar()
        {
            return CanGoToTarget()
                ? new PathFinding(_gridSize, _gridSize,
                        GetMonsterGridCoords(), GetTargetGridCoords(), _offsets, _obstacles)
                    .FindPath()
                    ?.Select(point => new Point(point.X - _offsets.X, point.Y - _offsets.Y))
                    .ToList()
                : null;
        }
    }
}