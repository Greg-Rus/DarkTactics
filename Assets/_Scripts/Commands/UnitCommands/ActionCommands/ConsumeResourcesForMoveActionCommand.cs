using _Scripts.Controllers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class ConsumeResourcesForMoveActionCommand : EventCommand
    {
        [Inject] public UnitStateController UnitStateController { private get; set; }

        public override void Execute()
        {
            if (UnitStateController.TryDeductActionPointsForAction(UnitActionTypes.Move) == false)
            {
                Fail();
            }
        }
    }
}