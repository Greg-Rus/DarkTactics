using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveSelectedUnitCommand : EventCommand
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

            MouseClickGroundPayload payload = (MouseClickGroundPayload)evt.data;
            var gridCellModel = GridService.WorldPositionToGridCellModel(payload.ClickPosition);

            if (gridCellModel == null) return;

            var unit = EntityRegistryService.GetEntityContextById(GameSessionModel.SelectedUnitId.Value);
            unit.dispatcher.Dispatch(GameEvents.GridCellSelected, gridCellModel);
        }
    }
}