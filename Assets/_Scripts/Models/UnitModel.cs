namespace _Scripts.Models
{
    public class UnitModel
    {
        public int Id;
        public UnityTypes UnitType;
        public GridCellModel OccupiedCellModel;
        public int MovementRange;
        public GridCellModel[,] WalkableCells;
        public UnitActionTypes[] SupportedActions;
        public UnitActionTypes SelectedAction = UnitActionTypes.None;

        public void InitializeWithSettings(UnitSettings settings)
        {
            UnitType = settings.Type;
            MovementRange = settings.MovementRange;
            SupportedActions = settings.Actions;
        }
    }
}