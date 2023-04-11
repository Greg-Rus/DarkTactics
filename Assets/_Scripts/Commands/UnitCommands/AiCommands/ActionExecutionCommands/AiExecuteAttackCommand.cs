using _Scripts.Controllers;
using _Scripts.EventPayloads;
using _Scripts.Helpers;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class AiExecuteAttackCommand : Command
    {
        private readonly AiAction _action;
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher EventDispatcher { private get; set; }
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        public AiExecuteAttackCommand(AiAction action)
        {
            _action = action;
        }

        public override void Execute()
        {
            Debug.Log($"{LogHelper.AITag}: Attack");
            
            EventDispatcher.Dispatch(InputEvents.AttackActionSelected);
            EventDispatcher.Dispatch(UnitEvents.EnemySelected, new AttackActionPayload()
            {
                TargetTransform = EntityRegistryService.GetTransformByEntityId(_action.TargetUnitId.Value),
                TargetId = _action.TargetUnitId.Value,
                TargetCoordinates = _action.TargetGridCellCoordinates.Value
            });        }

        
    }
}