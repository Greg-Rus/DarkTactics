namespace _Scripts.Models
{
    public class UnitModel
    {
        public int Id;
        public GridCellModel OccupiedCellModel;
        public GridCellModel[,] ActionRangeCells;
        public UnitActionTypes SelectedAction = UnitActionTypes.None;
        public UnitSettings Settings;
        public UnitModelState State;
        public bool IsAttacking;

        public void InitializeWithSettings(UnitSettings settings)
        {
            Settings = settings;
            State.HitPoints = settings.BaseHitPoints;
            State.ActionPoints = settings.BaseActionPoints;
            State.SpellPoints = settings.BaseSpellPoints;
        }

        public struct UnitModelState
        {
            public int HitPoints;
            public int ActionPoints;
            public int SpellPoints;
        }
    }
}