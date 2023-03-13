using _Scripts.Models;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class UnitSensor
    {
        [Inject] public UnitModel UnitModel { private get; set; }

        [Inject] public GridService GridService { private get; set; }
        
        public bool IsCellWalkable(GridCellModel cell)
        {
            return cell.Entities.Count == 0;
        }

        public bool IsCellInRange(GridCellModel cell)
        {
            return IsCellInRange(cell.Coordinates);
        }

        public bool IsCellInRange(Vector2 cellCoordinates)
        {
            var isCellInRange = false;
            foreach (var cellModel in UnitModel.ActionRangeCells)
            {
                if(cellModel == null) continue;
                if (cellModel.Coordinates != cellCoordinates) continue;
                
                isCellInRange = true;
                break;
            }

            return isCellInRange;
        }
    }
}