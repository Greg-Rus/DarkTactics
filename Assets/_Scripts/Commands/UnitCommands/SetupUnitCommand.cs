using System.IO;
using _Scripts.EventPayloads;
using _Scripts.Models;
using Shapes;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class SetupUnitCommand : EventCommand<SetupUnitPayload>
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GridService GridService { private get; set; }
        [Inject] public UnitContextRoot UnitContextRoot { private get; set; }
        
        public override void Execute()
        {
            var worldPosition = GridService.GridCoordinateToWorldPosition(Payload.GridPosition);
            UnitContextRoot.transform.position = worldPosition;
            
            UnitModel.InitializeWithSettings(Payload.Settings);
            UnitModel.OccupiedCellModel = GridService.GridCoordinateToGridCellModel(Payload.GridPosition);

            if (UnitModel.OccupiedCellModel == null)
            {
                throw new InvalidDataException($"{Payload.GridPosition} is outside the Grid");
            }
            
            UnitModel.OccupiedCellModel.Entities.Add(UnitModel.Id);

            dispatcher.Dispatch(UnitEvents.UnitSelected, new UnitSelectedPayload(){SelectedUnitId = -1});
        }
    }
}