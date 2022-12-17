using _Scripts.Commands.UnitCommands;
using _Scripts.EventPayloads;
using _Scripts.Helpers;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class ToggleUnitSelectionCommand : EventCommand<UnitSelectedPayload>
    {
        [Inject(UnitContextKeys.Id)]  public int UnitId {get;set;}
        [Inject] public UnitContextRoot RootView{get;set;}
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        public override void Execute()
        {
            var isThisUnitActive = UnitId == Payload.SelectedUnitId;
            RootView.SelectionMarker.SetActive(isThisUnitActive);
            if (isThisUnitActive)
            {
                new UpdateUiListenersCommand().InjectWith(injectionBinder).Execute();
                new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            }
            else
            {
                GridVisualsService.ClearGrid();
            }
        }
    }
}