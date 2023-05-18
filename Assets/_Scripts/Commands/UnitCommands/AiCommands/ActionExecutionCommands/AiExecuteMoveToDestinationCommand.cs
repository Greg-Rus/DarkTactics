using _Scripts.Controllers;
using _Scripts.Helpers;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class AiExecuteMoveToDestinationCommand : Command
    {
        private readonly AiAction _action;
        [Inject] public GridService GridService { private get; set; }
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher EventDispatcher { private get; set; }

        public AiExecuteMoveToDestinationCommand(AiAction action)
        {
            _action = action;
        }

        public override void Execute()
        {
            Debug.Log($"{LogHelper.AITag}: Moving to {_action.TargetTileCoordinates.Value}");
            
            EventDispatcher.Dispatch(InputEvents.MoveActionSelected);
            EventDispatcher.Dispatch(UnitEvents.TileSelected, GridService.GridCoordinateToTileModel(_action.TargetTileCoordinates.Value));
        }
    }
}