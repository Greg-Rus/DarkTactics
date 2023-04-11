using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CanPerformMoveActionCommand : EventCommand<GridCellModel>
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public UnitSensor UnitSensor { private get; set; }
        public override void Execute()
        {
            if (IsActionTypeMove &&
                HasEnoughActionPoints &&
                DestinationCellIsWalkable &&
                DestinationCellIsInRange)
            {
                return;
            }

            Fail();
        }

        private bool IsActionTypeMove => UnitModel.SelectedAction == UnitActionType.Move;
        private bool HasEnoughActionPoints => UnitStateController.CanPerformAction(UnitActionType.Move);
        private bool DestinationCellIsWalkable => UnitSensor.IsCellWalkable(Payload);
        private bool DestinationCellIsInRange => UnitSensor.IsCellInRange(Payload);
    }
}

