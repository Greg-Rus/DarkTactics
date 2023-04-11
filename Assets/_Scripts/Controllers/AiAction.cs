using _Scripts.Models;
using UnityEngine;

namespace _Scripts.Controllers
{
    public struct AiAction
    {
        public UnitActionType ActionType;
        public Vector2Int? TargetGridCellCoordinates;
        public int? TargetUnitId;
        public float Score;
    }
}