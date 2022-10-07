using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class UpdateWalkableCellsCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            UnitModel.WalkableCells = GridService.GetCellsInRange(UnitModel.Settings.MovementRange, UnitModel.OccupiedCellModel.Coordinates, IsCellWalkable);
        }

        private bool IsCellWalkable(GridCellModel cell)
        {
            return cell.Entities.Count == 0;
        }
    }
}