using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands
{
    public class EndEnemyTurnCommand : EventCommand
    {
        public override void Execute()
        {
            dispatcher.Dispatch(GameEvent.StartPlayerTurn);
        }
    }
}