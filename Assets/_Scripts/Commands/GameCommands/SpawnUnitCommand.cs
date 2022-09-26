using _Scripts.EventPayloads;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class SpawnUnitCommand : EventCommand
    {
        [Inject] public PrefabConfig Config {get;set;}
        [Inject] public UnitRegistryService UnitRegistryService {get;set;}
        [Inject] public GameContextRoot GameContextRoot { get; set; }

        public override void Execute()
        {
            SpawnEventPayload payload = (SpawnEventPayload)evt.data;
            var id = payload.Id;
            var position = payload.InitialPosition;
            
            var unitContextView = Object.Instantiate(Config.Unit, GameContextRoot.UnitsRoot, true);
            var unitContext = new UnitContext(unitContextView, true, id, position);

            unitContextView.context = unitContext;
            unitContextView.transform.position = new Vector3(position.x, 0f, position.y);
            unitContext.Start();
            
            UnitRegistryService.RegisterUnit(id, unitContext, unitContextView.transform);
        }
    }
}