using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CanPerformMoveActionCommand : EventCommand<TileModel>
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public UnitSensor UnitSensor { private get; set; }
        public override void Execute()
        {
            if (UnitStateController.IsAlive &&
                IsActionTypeMove &&
                HasEnoughActionPoints &&
                DestinationTileIsWalkable &&
                DestinationTileIsInRange)
            {
                return;
            }

            Fail();
        }

        private bool IsActionTypeMove => UnitModel.SelectedAction == UnitActionType.Move;
        private bool HasEnoughActionPoints => UnitStateController.CanPerformAction(UnitActionType.Move);
        private bool DestinationTileIsWalkable => UnitSensor.IsTileWalkable(Payload);
        private bool DestinationTileIsInRange => UnitSensor.IsTileInRange(Payload);
    }
}

