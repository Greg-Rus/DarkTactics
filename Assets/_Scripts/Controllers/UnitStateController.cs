using System;
using System.Linq;
using _Scripts.Models;

namespace _Scripts.Controllers
{
    public class UnitStateController
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public ActionSettingsConfig ActionSettingsConfig { private get; set; }

        public bool CanPerformAction(UnitActionTypes action)
        {
            var supportsAction = UnitModel.Settings.Actions.Contains(action);
            var hasEnoughActionPoints = UnitModel.State.ActionPoints >= ActionSettingsConfig.GetActionCost(action);
            return supportsAction && hasEnoughActionPoints;
        }

        public bool TryDeductActionPointsForAction(UnitActionTypes action)
        {
            if (CanPerformAction(action))
            {
                UnitModel.State.ActionPoints -= ActionSettingsConfig.GetActionCost(action);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeUnitHitPointsByAmount(int changeAmount)
        {
            UnitModel.State.HitPoints = Math.Max(UnitModel.State.HitPoints + changeAmount, 0);
        }
        
        public void ChangeUnitActionPointsByAmount(int changeAmount)
        {
            UnitModel.State.ActionPoints = Math.Max(UnitModel.State.ActionPoints + changeAmount, 0);
        }

        public void ChangeUnitSpellPointsByAmount(int changeAmount)
        {
            UnitModel.State.SpellPoints = Math.Max(UnitModel.State.SpellPoints + changeAmount, 0);
        }
    }
}