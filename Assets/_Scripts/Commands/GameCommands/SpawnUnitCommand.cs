using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class SpawnUnitCommand : EventCommand<SpawnEventPayload>
    {
        [Inject] public PrefabConfig Config {get;set;}
        [Inject] public UnitRegistryService UnitRegistryService {get;set;}
        [Inject] public GameContextRoot GameContextRoot { get; set; }

        public override void Execute()
        {

            var id = Payload.Id;
            
            var unitContextView = Object.Instantiate(Config.Unit, GameContextRoot.UnitsRoot, true);
            var unitContext = new UnitContext(unitContextView, true, id);

            unitContextView.context = unitContext;
            injectionBinder.injector.Inject(unitContext);
            unitContext.Start();
            
            unitContext.dispatcher.Dispatch(GameEvents.SetupUnit, new SetupUnitPayload()
            {
                GridPosition = Payload.InitialPosition,
                Settings = Payload.Settings
            });
            
            UnitRegistryService.RegisterUnit(id, unitContext, unitContextView.transform);
        }
    }
}