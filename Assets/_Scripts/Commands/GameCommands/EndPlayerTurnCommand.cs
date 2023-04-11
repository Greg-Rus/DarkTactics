using _Scripts.Models;
using _Scripts.Services;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class EndPlayerTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }

        [Inject] public EnemyTurnService EnemyTurnService { private get; set; }
        public override void Execute()
        {
            UiController.ToggleEndTurnButton(false);
            UiController.ToggleActionsBar(false);
            UiController.ToggleUnitStats(false);
            
            dispatcher.Dispatch(GameEvent.StartEnemyTurn);
        }
    }
}