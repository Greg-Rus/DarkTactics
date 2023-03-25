using System.Linq;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class StartPlayerTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }

        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }
        public override void Execute()
        {
            GameSessionModel.TurnNumber++;
            GameSessionModel.PlayersTurn = true;
            UiController.UpdateTurnNumber(GameSessionModel.TurnNumber);
            UiController.ToggleEndTurnButton(true);

            foreach (var id in EntityRegistryService.GetAllPlayerUnitId())
            {
                EntityRegistryService.GetFasadeById(id)
                    .EventDispatcher
                    .Dispatch(UnitEvents.TurnStarted);
            }
        }
    }
}