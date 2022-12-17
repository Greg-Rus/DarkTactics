using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CompleteUnitTurnCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            UiController.SetAllActionButtonsInteractable(false);
        }
    }
}