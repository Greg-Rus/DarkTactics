using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveSelectedUnitCommand : EventCommand
    {
        [Inject] public UnitManager UnitManager {get;set;}
        public override void Execute()
        {
            var unit = UnitManager.GetUnitById(1);
            Debug.Log("Dispatching move unit at context");
            unit.dispatcher.Dispatch(GameEvents.MoveUnit, new Vector3(4f, 0f, 4f));
        }
    }
}