using _Scripts.Controllers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class UpdateUnitStatsUiCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }

        public override void Execute()
        {
            UiController.UpdateUnitHitPoints(UnitModel.HitPoints, UnitModel.Settings.BaseHitPoints);
            UiController.UpdateUnitActionPoints(UnitModel.ActionPoints, UnitModel.Settings.BaseActionPoints);
            UiController.UpdateUnitSpellPoints(UnitModel.SpellPoints, UnitModel.Settings.BaseSpellPoints);
            
            foreach (var action in UnitModel.Settings.Actions)
            {
                UiController.SetActionButtonInteractable(action, UnitStateController.CanPerformAction(action));
            }
        }
    }
}