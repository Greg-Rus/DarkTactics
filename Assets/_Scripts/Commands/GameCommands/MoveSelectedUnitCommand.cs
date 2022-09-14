using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveSelectedUnitCommand : EventCommand
    {
        [Inject] public UnitRegistryService UnitRegistryService {get;set;}
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
            var gridPosition = GridService.WorldPositionToWorldGridCell(payload.ClickPosition);

            var unit = UnitRegistryService.GetUnitContextById(GameSessionModel.SelectedUnitId.Value);
            unit.dispatcher.Dispatch(GameEvents.MoveUnit, gridPosition);
        }
    }
}