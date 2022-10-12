using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands
{
    public class ProcessUnitClickCommand : EventCommand
    {
        [Inject] public EntityRegistryService EntityRegistryService { get; set; }
        [Inject(ContextKeys.CROSS_CONTEXT_DISPATCHER)] public IEventDispatcher crossContextDispatcher { get; set; }
        

        public override void Execute()
        {
            var payload = (MouseClickUnitPayload)evt.data;
            var selectedUnitId = EntityRegistryService.GetEntityIdByTransform(payload.UnitTransform);
            crossContextDispatcher.Dispatch(GameEvents.SelectUnit, new UnitSelectedPayload { SelectedUnitId = selectedUnitId });
        }
    }
}