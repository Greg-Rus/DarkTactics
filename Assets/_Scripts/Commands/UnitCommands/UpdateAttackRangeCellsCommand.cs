using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class UpdateAttackRangeTilesCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            UnitModel.ActionRangeTiles = GridService.GetTilesInRange(UnitModel.Settings.AttackRange, UnitModel.OccupiedTileModel.Coordinates, IsTileAttackable);
        }

        private bool IsTileAttackable(TileModel tile)
        {
            return true;
        }
    }
}