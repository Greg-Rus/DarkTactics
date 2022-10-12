using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands
{
    public class ProcessEnemyClickCommand : EventCommand<AttackActionPayload>
    {
        [Inject] public EntityRegistryService EntityRegistryService { get; set; }
        [Inject] public GameSessionModel GameSessionModel { private get; set; }

        public override void Execute()
        {
            if (GameSessionModel.SelectedUnitId.HasValue)
            {
                EntityRegistryService.GetEntityContextById(GameSessionModel.SelectedUnitId.Value)
                    .dispatcher
                    .Dispatch(UnitEvents.EnemySelected, Payload);
            }
        }
    }
}