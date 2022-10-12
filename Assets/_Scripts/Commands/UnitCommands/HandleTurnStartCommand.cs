using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleTurnStartCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        public override void Execute()
        {
            UnitModel.State.ActionPoints = UnitModel.Settings.BaseActionPoints;
            if (GameSessionModel.SelectedUnitId.HasValue && GameSessionModel.SelectedUnitId.Value == UnitModel.Id)
            {
                new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            }
        }
    }
}