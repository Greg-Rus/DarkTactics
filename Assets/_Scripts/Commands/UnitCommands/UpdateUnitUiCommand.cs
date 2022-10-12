using _Scripts.Controllers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class UpdateUnitUiCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }

        public override void Execute()
        {
            UiController.UpdateUnitHitPoints(UnitModel.State.HitPoints, UnitModel.Settings.BaseHitPoints);
            UiController.UpdateUnitActionPoints(UnitModel.State.ActionPoints, UnitModel.Settings.BaseActionPoints);
            UiController.UpdateUnitSpellPoints(UnitModel.State.SpellPoints, UnitModel.Settings.BaseSpellPoints);
            
            foreach (var action in UnitModel.Settings.Actions)
            {
                UiController.SetActionButtonInteractable(action, UnitStateController.CanPerformAction(action));
            }
        }
    }
}