using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Monster
{
    public class Searcher
    {
        private readonly GameObject _monster;
        private readonly GameObject _target;
        
        private readonly int _gridSize;

        private readonly int2 _offsets; 
    
        public Searcher(GameObject target, GameObject monster, int gridSize)
        {
            _target = target;
            _monster = monster;
            _gridSize = gridSize;
            _offsets = FindOffset();
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
                        GetMonsterGridCoords(), GetTargetGridCoords(), _offsets)
                    .FindPath()
                    ?.Select(point => new int2(point.x - _offsets.x, point.y - _offsets.y))
                    .ToList()
                : null;
        }
    }
}