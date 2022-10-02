using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using strange.extensions.injector.api;
using UnityEngine;

namespace _Scripts.Commands
{
    public class SpawnUnitCommand : EventCommand
    {
        [Inject] public PrefabConfig Config {get;set;}
        [Inject] public UnitRegistryService UnitRegistryService {get;set;}
        [Inject] public GameContextRoot GameContextRoot { get; set; }
        [Inject] public GridService GridService { private get; set; }

        public override void Execute()
        {
            SpawnEventPayload payload = (SpawnEventPayload)evt.data;
            var id = payload.Id;
            
            var unitContextView = Object.Instantiate(Config.Unit, GameContextRoot.UnitsRoot, true);
            var unitContext = new UnitContext(unitContextView, true, id);

            unitContextView.context = unitContext;
            injectionBinder.injector.Inject(unitContext);
            unitContext.Start();
            
            unitContext.dispatcher.Dispatch(GameEvents.SetupUnit, new SetupUnitPayload()
            {
                GridPosition = payload.InitialPosition,
                MovementRange = 5
            });
            
            UnitRegistryService.RegisterUnit(id, unitContext, unitContextView.transform);
        }
    }
}