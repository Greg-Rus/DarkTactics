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
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            if (!GameSessionModel.SelectedUnitId.HasValue) return;
            var enemyId = EntityRegistryService.GetEntityIdByTransform(Payload.TargetTransform);
            var enemyCoordinate = GridService.GetUnitCoordinatesByUnitId(enemyId);
                
            EntityRegistryService.GetFasadeById(GameSessionModel.SelectedUnitId.Value)
                .EventDispatcher
                .Dispatch(UnitEvents.EnemySelected, new AttackActionPayload()
                {
                    TargetTransform = Payload.TargetTransform,
                    TargetId = enemyId,
                    TargetCoordinates = enemyCoordinate
                });
        }
    }
}