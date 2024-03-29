namespace _Scripts.Models
{
    public class UnitModel : EntityModel
    {
        public TileModel[,] ActionRangeTiles;
        public UnitActionType SelectedAction = UnitActionType.None;
        public UnitSettings Settings;
        public UnitModelState State;
        public bool IsAttacking;
        
        public void InitializeWithSettings(UnitSettings settings)
        {
            Settings = settings;
            State.HitPoints = settings.BaseHitPoints;
            State.ActionPoints = settings.BaseActionPoints;
            State.SpellPoints = settings.BaseSpellPoints;
            EntityType = Settings.EntityType;
        }

        public struct UnitModelState
        {
            public int HitPoints;
            public int ActionPoints;
            public int SpellPoints;
        }
    }
}