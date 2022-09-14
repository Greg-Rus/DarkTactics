using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class InitializeServicesCommand : EventCommand
    {
        [Inject] public InputService InputService { private get; set; }
        public override void Execute()
        {
            InputService.Initialize();
        }
    }
}