using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands
{
    public class ProcessUnitClickCommand : EventCommand
    {
        [Inject] public EntityRegistryService EntityRegistryService { get; set; }
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }

        public override void Execute()
        {
            var payload = (MouseClickUnitPayload)evt.data;
            var selectedUnitId = EntityRegistryService.GetEntityIdByTransform(payload.UnitTransform);

            TryDeselectOldUnit(selectedUnitId);

            UiController.ToggleUnitStats(true);
            UiController.ToggleActionsBar(true);
            UiController.ResetActionButtons();
            
            SelectNewUnit(selectedUnitId);
        }

        private void SelectNewUnit(int selectedUnitId)
        {
            GameSessionModel.SelectedUnitId = selectedUnitId;
            
            EntityRegistryService.GetFasadeById(selectedUnitId)
                .EventDispatcher
                .Dispatch(UnitEvents.UnitSelected, new UnitSelectedPayload { SelectedUnitId = selectedUnitId });
        }

        private void TryDeselectOldUnit(int selectedUnitId)
        {
            if (GameSessionModel.SelectedUnitId.HasValue)
            {
                var oldUnit = GameSessionModel.SelectedUnitId.Value;
                EntityRegistryService.GetFasadeById(oldUnit)
                    .EventDispatcher
                    .Dispatch(UnitEvents.UnitSelected, new UnitSelectedPayload { SelectedUnitId = selectedUnitId });
            }
        }
    }
}