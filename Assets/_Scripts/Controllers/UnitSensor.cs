using _Scripts.Models;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class UnitSensor
    {
        [Inject] public UnitModel UnitModel { private get; set; }

        [Inject] public GridService GridService { private get; set; }
        
        public bool IsTileWalkable(TileModel tile)
        {
            return tile.Entities.Count == 0;
        }

        public bool IsTileInRange(TileModel tile)
        {
            return IsTileInRange(tile.Coordinates);
        }

        public bool IsTileInRange(Vector2 tileCoordinates)
        {
            var isTileInRange = false;
            foreach (var tileModel in UnitModel.ActionRangeTiles)
            {
                if(tileModel == null) continue;
                if (tileModel.Coordinates != tileCoordinates) continue;
                
                isTileInRange = true;
                break;
            }

            return isTileInRange;
        }
    }
}