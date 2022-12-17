using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class UpdateAttackRangeCellsCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            UnitModel.ActionRangeCells = GridService.GetCellsInRange(UnitModel.Settings.AttackRange, UnitModel.OccupiedCellModel.Coordinates, IsCellAttackable);
        }

        private bool IsCellAttackable(GridCellModel cell)
        {
            return true;
        }
    }
}