using _Scripts.Commands.UnitCommands;
using _Scripts.EventPayloads;
using _Scripts.EventPayloads.UnitEventPayloads;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class ToggleUnitSelectionCommand : EventCommand
    {
        [Inject(UnitContextKeys.Id)]  public int UnitId {get;set;}
        [Inject] public UnitContextRoot RootView{get;set;}
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        public override void Execute()
        {
            var selectionResult = (SelectUnitPayload)evt.data;
            var isThisUnitActive = UnitId == selectionResult.SelectedUnitId;
            RootView.SelectionMarker.SetActive(isThisUnitActive);
            if (isThisUnitActive)
            {
                new UpdateUiListenersCommand().InjectWith(injectionBinder).Execute();
            }
            else
            {
                GridVisualsService.ClearWalkableGrid();
            }
        }
    }
}