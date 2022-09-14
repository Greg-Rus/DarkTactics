using System.Linq;
using _Scripts.EventPayloads;
using _Scripts.EventPayloads.UnitEventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class SelectUnitCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { get; private set; }
        [Inject] public UnitRegistryService UnitRegistryService { get; private set; }
        public override void Execute()
        {
            var selectUnitPayload = (SelectUnitPayload)evt.data;
            var selectedUnitId = selectUnitPayload.SelectedUnitId;
            GameSessionModel.SelectedUnitId = selectedUnitId;
            var allUnitIds = UnitRegistryService.GetAllUnitIds().Except(new []{selectedUnitId});
            foreach (var unitId in allUnitIds)
            {
                UnitRegistryService.GetUnitContextById(unitId).dispatcher.Dispatch(UnitEvents.UnitSelected, new UnitSelectionResultPayload(){IsSelected = false});
            }
            UnitRegistryService.GetUnitContextById(selectedUnitId).dispatcher.Dispatch(UnitEvents.UnitSelected, new UnitSelectionResultPayload(){IsSelected = true});
        }
    }
}