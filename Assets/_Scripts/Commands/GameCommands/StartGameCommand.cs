using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands
{
    public class StartGameCommand : EventCommand
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { get; set; }
        [Inject] public GridService GridService { get; set; }
        public override void Execute()
        {
            GridService.Initialize();
            Dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload(){Id = 1, InitialPosition = new Vector2Int(1,0)});
            Dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload(){Id = 2, InitialPosition = new Vector2Int(4,0)});
        }
    }
}