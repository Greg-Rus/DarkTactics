using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveSelectedUnitCommand : EventCommand<MouseClickGroundPayload>
    {
        [Inject] public EntityRegistryService EntityRegistryService {get;set;}
        [Inject] public GameSessionModel GameSessionModel {get;set;}
        [Inject] public GridService GridService {get;set;}
        public override void Execute()
        {
            if (!GameSessionModel.SelectedUnitId.HasValue)
            {
                Fail();
                return;
            }
            
            var gridCellModel = GridService.WorldPositionToGridCellModel(Payload.ClickPosition);

            if (gridCellModel == null) return;

            var unit = EntityRegistryService.GetFasadeById(GameSessionModel.SelectedUnitId.Value);
            unit.EventDispatcher.Dispatch(UnitEvents.GridCellSelected, gridCellModel);
        }
    }
}