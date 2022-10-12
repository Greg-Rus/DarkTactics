using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using Object = UnityEngine.Object;

namespace _Scripts.Commands
{
    public class SpawnUnitCommand : EventCommand<SpawnEventPayload>
    {
        [Inject] public PrefabConfig UnitPrefabConfig {get;set;}
        [Inject] public EntityRegistryService EntityRegistryService {get;set;}
        [Inject] public GameContextRoot GameContextRoot { get; set; }

        public override void Execute()
        {
            var id = Payload.Id;

            var prefab = UnitPrefabConfig.GetPrefabForUnitType(Payload.Settings.UnitType);
            var unitContextView = Object.Instantiate(prefab, GameContextRoot.UnitsRoot, true);
            var unitContext = new UnitContext(unitContextView, true, id);

            unitContextView.context = unitContext;
            injectionBinder.injector.Inject(unitContext);
            unitContext.Start();
            
            unitContext.dispatcher.Dispatch(GameEvents.SetupUnit, new SetupUnitPayload()
            {
                GridPosition = Payload.InitialPosition,
                Settings = Payload.Settings
            });
            
            EntityRegistryService.RegisterEntity(id, unitContext, unitContextView.transform, EntityTypes.PlayerUnit);
        }
    }
}