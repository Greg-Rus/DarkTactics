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
            var hasEnoughActionPoints = UnitModel.ActionPoints >= ActionSettingsConfig.GetActionCost(action);
            return supportsAction && hasEnoughActionPoints;
        }

        public bool TryDeductActionPointsForAction(UnitActionTypes action)
        {
            if (CanPerformAction(action))
            {
                UnitModel.ActionPoints -= ActionSettingsConfig.GetActionCost(action);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ChangeUnitHitPointsByAmount(int changeAmount)
        {
            UnitModel.HitPoints = Math.Max(UnitModel.HitPoints + changeAmount, 0);
        }
        
        public void ChangeUnitActionPointsByAmount(int changeAmount)
        {
            UnitModel.ActionPoints = Math.Max(UnitModel.ActionPoints + changeAmount, 0);
        }

        public void ChangeUnitSpellPointsByAmount(int changeAmount)
        {
            UnitModel.SpellPoints = Math.Max(UnitModel.SpellPoints + changeAmount, 0);
        }
    }
}