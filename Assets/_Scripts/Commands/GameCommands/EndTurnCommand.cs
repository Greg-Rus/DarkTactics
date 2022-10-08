using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands
{
    public class EndTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            GameSessionModel.TurnNumber++;
            UiController.UpdateTurnNumber(GameSessionModel.TurnNumber);
            //TODO: This would be the trigger for AI units to start acting.
        }
    }
}