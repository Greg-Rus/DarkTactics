using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleMoveActionEndCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        
        public override void Execute()
        {
            UnitModel.SelectedAction = UnitActionTypes.None;
            UnitModel.ActionRangeCells = new GridCellModel[0,0];
            GridVisualsService.ClearGrid();
            UiController.MoveActionButton.Highlight.gameObject.SetActive(false);
        }
    }
}