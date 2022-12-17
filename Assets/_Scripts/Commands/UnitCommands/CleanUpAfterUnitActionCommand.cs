using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CleanUpAfterUnitActionCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }

        public override void Execute()
        {
            UiController.SetActionInProgressUi(false);
        }
    }
}