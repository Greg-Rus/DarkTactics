using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class PrepareForUnitActionCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }

        [Inject] public GridVisualsService GridVisualsService { private get; set; }

        [Inject] public UnitModel UnitModel { private get; set; }

        public override void Execute()
        {
            GridVisualsService.ClearGrid();
            UnitModel.SelectedAction = UnitActionTypes.None;
            UiController.SetActionInProgressUi(true);
        }
    }
}