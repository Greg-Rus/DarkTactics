using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CompleteMoveActionCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        
        public override void Execute()
        {
            UnitModel.SelectedAction = UnitActionType.None;
            UnitModel.ActionRangeTiles = new TileModel[0,0];
            GridVisualsService.ClearGrid();
            UiController.MoveActionButton.Highlight.gameObject.SetActive(false);
        }
    }
}