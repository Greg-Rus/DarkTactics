using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands
{
    public class StartGameCommand : EventCommand
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { get; set; }
        public override void Execute()
        {
            Dispatcher.Dispatch(GameEvents.SpawnUnit, 1);
            Dispatcher.Dispatch(GameEvents.SpawnUnit, 2);
        }
    }
}