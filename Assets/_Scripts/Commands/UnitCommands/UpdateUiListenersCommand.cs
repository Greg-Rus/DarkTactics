using System;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Commands.UnitCommands
{
    public class UpdateUiListenersCommand : Command
    {
        private readonly bool shouldListen;
        [Inject] public MainUiController MainUiController { private get; set; }
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }

        public override void Execute()
        {
            MainUiController.RemoveAllListeners();
            
            foreach (var action in UnitModel.SupportedActions)
            {
                switch (action)
                {
                    case UnitActionTypes.Move:
                        MainUiController.AddMoveActionListener(Dispatcher);
                        break;
                    case UnitActionTypes.Attack:
                        MainUiController.AddAttackActionListener(Dispatcher);
                        break;
                    case UnitActionTypes.EndTurn:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}