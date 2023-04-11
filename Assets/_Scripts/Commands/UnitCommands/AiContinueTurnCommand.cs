using _Scripts.Controllers;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class AiContinueTurnCommand : Command
    {
        [Inject] public UnitBrain UnitBrain { private get; set; }
        public override void Execute()
        {
            UnitBrain.TakeNextAction();
        }
    }
}