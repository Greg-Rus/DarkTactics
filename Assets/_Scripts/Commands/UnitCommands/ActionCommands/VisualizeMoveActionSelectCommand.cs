using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class VisualizeMoveActionSelectCommand : EventCommand
    {
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }

        public override void Execute()
        {
            GridVisualsService.DrawWalkableGrid(UnitModel.ActionRangeCells);
            UiController.HighlightSelectedAction(UnitActionTypes.Move);
        }
    }
}