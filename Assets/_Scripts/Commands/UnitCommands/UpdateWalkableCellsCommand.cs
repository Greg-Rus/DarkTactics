using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class UpdateWalkableTilesCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            UnitModel.ActionRangeTiles = GridService.GetTilesInRange(UnitModel.Settings.MovementRange, UnitModel.OccupiedTileModel.Coordinates, IsTileWalkable);
        }

        private bool IsTileWalkable(TileModel tile)
        {
            return tile.Entities.Count == 0;
        }
    }
}