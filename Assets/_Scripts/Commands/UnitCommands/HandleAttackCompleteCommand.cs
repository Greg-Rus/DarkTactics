using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleAttackCompleteCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            UnitModel.SelectedAction = UnitActionTypes.None;
            UiController.HighlightSelectedAction(UnitActionTypes.None);
            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
        }
    }
}