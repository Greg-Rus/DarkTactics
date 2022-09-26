using UnityEngine;

namespace _Scripts.Models
{
    public interface IUnitModel
    {
        
    }
    
    public class UnitModel
    {
        public int Id;
        public Vector2Int OccupiedCellCoordinates;
        public int MovementRange;
    }
}