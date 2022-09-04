using strange.extensions.context.api;
using strange.extensions.command.impl;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class SpawnUnitCommand : EventCommand
    {
        [Inject] public PrefabConfig Config {get;set;}
        
        [Inject] public UnitManager UnitManager {get;set;}
        [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject contextView{get;set;}

        public override void Execute()
        {
            int id = (int)evt.data;
            
            var unitContextView = Object.Instantiate(Config.Unit, contextView.transform, true);
            var unitContext = new UnitContext(unitContextView, true, id);
            //injectionBinder.injector.Inject(unitContext, false); //it is possible to inject from above.
            unitContextView.context = unitContext;
            unitContextView.transform.position = new Vector3(id, 0f, 0f);
            unitContext.Start();
            
            UnitManager.RegisterUnit(id, unitContext);
        }
    }
}