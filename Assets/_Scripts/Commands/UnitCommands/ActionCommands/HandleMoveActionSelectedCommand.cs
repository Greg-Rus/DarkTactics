using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleMoveActionSelectedCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }

        public override void Execute()
        {
            if (UnitStateController.CanPerformAction(UnitActionTypes.Move))
            {
                UnitModel.SelectedAction = UnitActionTypes.Move;
                new UpdateWalkableCellsCommand().InjectWith(injectionBinder).Execute();
            }
            else
            {
                Cancel();
            }
        }
    }
}