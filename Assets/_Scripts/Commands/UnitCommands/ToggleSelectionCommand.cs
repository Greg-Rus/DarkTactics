using _Scripts.EventPayloads;
using _Scripts.EventPayloads.UnitEventPayloads;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class ToggleSelectionCommand : EventCommand
    {
        [Inject(UnitContextKeys.Id)]  public int UnitId {get;set;}
        [Inject]  public UnitContextRoot RootView{get;set;}
        public override void Execute()
        {
            var selectionResult = (SelectUnitPayload)evt.data;
            RootView.SelectionMarker.SetActive(UnitId == selectionResult.SelectedUnitId);
        }
    }
}