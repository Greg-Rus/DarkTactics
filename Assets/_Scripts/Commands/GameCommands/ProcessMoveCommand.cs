using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands
{
    public class ProcessMoveCommand : EventCommand
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { private get; set; }
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        public override void Execute()
        {
            var destination = (Vector3)evt.data;
            if (GameSessionModel.SelectedUnitId.HasValue)
            {
                Dispatcher.Dispatch(GameEvents.MoveUnit, new MoveSelectedUnitPayload(){Destination = destination, UnitId = GameSessionModel.SelectedUnitId.Value});
            }
            else
            {
                Debug.Log($"Tried to execute move to {destination}, but no unit is selected. Ignoring.");
            }
        }
    }
}