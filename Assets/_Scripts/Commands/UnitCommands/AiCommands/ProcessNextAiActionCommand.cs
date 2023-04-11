using _Scripts.Controllers;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class ProcessNextAiActionCommand : Command
    {
        [Inject] public UnitBrain UnitBrain { private get; set; }
        public override void Execute()
        {
            UnitBrain.TakeNextAction();
        }
    }
}