namespace _Scripts.Models
{
    public class UnitModel
    {
        public int Id;
        public GridCellModel OccupiedCellModel;
        public GridCellModel[,] WalkableCells;
        public UnitActionTypes SelectedAction = UnitActionTypes.None;
        public UnitSettings Settings;
        public int HitPoints;
        public int ActionPoints;
        public int SpellPoints;

        public void InitializeWithSettings(UnitSettings settings)
        {
            Settings = settings;
            HitPoints = settings.BaseHitPoints;
            ActionPoints = settings.BaseActionPoints;
            SpellPoints = settings.BaseSpellPoints;
        }
    }
}