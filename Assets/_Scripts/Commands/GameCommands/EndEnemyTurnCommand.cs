using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class EndEnemyTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            dispatcher.Dispatch(GameEvents.StartPlayerTurn);
        }
    }
}