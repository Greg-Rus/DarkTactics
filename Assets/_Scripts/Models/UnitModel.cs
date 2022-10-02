using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Models
{
    public interface IUnitModel
    {
        
    }
    
    public class UnitModel
    {
        public int Id;
        public EntityTypes UnitType;
        public GridCellModel OccupiedCellModel;
        public int MovementRange;
        public GridCellModel[,] WalkableCells;
    }
}