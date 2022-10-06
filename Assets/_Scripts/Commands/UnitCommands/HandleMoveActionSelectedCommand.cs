using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleMoveActionSelectedCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        public override void Execute()
        {
            UnitModel.SelectedAction = UnitActionTypes.Move;
            new UpdateWalkableCellsCommand().InjectWith(injectionBinder).Execute();
            GridVisualsService.DrawWalkableGrid(UnitModel.WalkableCells);
        }
    }
}