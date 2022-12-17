using System;
using System.Linq;
using _Scripts.Commands.UnitCommands;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.injector.api;

namespace _Scripts.Controllers
{
    public class UnitStateController
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public ActionSettingsConfig ActionSettingsConfig { private get; set; }
        
        [Inject] public IInjectionBinder InjectionBinder{ get; set; }

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
                CheckForEndOfTurn();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CheckForEndOfTurn()
        {
            if (UnitModel.State.ActionPoints <= 0)
            {
                new CompleteUnitTurnCommand().InjectWith(InjectionBinder).Execute();
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