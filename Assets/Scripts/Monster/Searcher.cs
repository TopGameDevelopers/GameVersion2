using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Monster
{
    public class Searcher
    {
        private readonly Vector3 _position;
        private readonly Vector3 _monsterPosition;
        private readonly int _gridSize;
        private readonly int2 _offsets;
        private readonly int2[] _obstacles;
    
        public Searcher(Vector3 target, Vector3 monster, int gridSize, int2[] obstacles)
        {
            _position = target;
            _monsterPosition = monster;
            _gridSize = gridSize;
            _offsets = FindOffset();
            _obstacles = obstacles;
        }

        private int2 GetMonsterActualCoords()
        {
            var xActual = (int) _monsterPosition.x;
            var yActual = (int) _monsterPosition.y;
            return new int2(xActual, yActual);
        }

        private int2 GetTargetActualCoords()
        {
            var xCoord = (int)_position.x;
            var yCoord = (int)_position.y;
            return new int2(xCoord, yCoord);
        }
    
        private int2 GetTargetGridCoords()
        {
            var targetActualCoord = GetTargetActualCoords();
            return new int2(targetActualCoord.x + _offsets.x, targetActualCoord.y + _offsets.y);
        }

        private int2 GetMonsterGridCoords()
        {
            return new int2(_gridSize / 2, _gridSize / 2);
        }

        private int2 FindOffset()
        {
            var monsterActualCoords = GetMonsterActualCoords();
            var monsterGridCoords = GetMonsterGridCoords();
            var xOffset = monsterGridCoords.x - monsterActualCoords.x;
            var yOffset = monsterGridCoords.y - monsterActualCoords.y;
            return new int2(xOffset, yOffset);
        }

        private bool CanGoToTarget()
        {
            var targetGridCoords = GetTargetGridCoords();
            return targetGridCoords.x >= 0 &&
                   targetGridCoords.x < _gridSize &&
                   targetGridCoords.y >= 0 &&
                   targetGridCoords.y < _gridSize;
        }

        public List<int2> GetPathAStar()
        {
            return CanGoToTarget()
                ? new PathFinding(_gridSize, _gridSize,
                        GetMonsterGridCoords(), GetTargetGridCoords(), _offsets, _obstacles)
                    .FindPath()
                    ?.Select(point => new int2(point.x - _offsets.x, point.y - _offsets.y))
                    .ToList()
                : null;
        }
    }
}