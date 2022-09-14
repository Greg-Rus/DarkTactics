using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
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
            int id = (int)evt.data;
            
            var unitContextView = Object.Instantiate(Config.Unit, GameContextRoot.UnitsRoot, true);
            var unitContext = new UnitContext(unitContextView, true, id);

            unitContextView.context = unitContext;
            unitContextView.transform.position = new Vector3(id, 0f, 0f);
            unitContext.Start();
            
            UnitRegistryService.RegisterUnit(id, unitContext, unitContextView.transform);
        }
    }
}