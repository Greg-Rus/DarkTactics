using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class VisualizeAttackSelectionCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        public override void Execute()
        {
            //new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute(); TODO: Not sure if necessary.
            UiController.HighlightSelectedAction(UnitActionType.Attack);
            GridVisualsService.DrawAttacableGrid(UnitModel.ActionRangeCells);
        }
    }
}