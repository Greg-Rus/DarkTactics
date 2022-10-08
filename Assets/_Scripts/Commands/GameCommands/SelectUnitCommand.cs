using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class SelectUnitCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { get; private set; }
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            var selectUnitPayload = (UnitSelectedPayload)evt.data;
            var selectedUnitId = selectUnitPayload.SelectedUnitId;
            GameSessionModel.SelectedUnitId = selectedUnitId;

            UiController.ToggleUnitStats(true);
        }
    }
}