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
        [Inject] public UiController UiController { private get; set; }
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }

        public override void Execute()
        {
            UiController.RemoveAllListeners();
            
            foreach (var action in UnitModel.Settings.Actions)
            {
                switch (action)
                {
                    case UnitActionType.Move:
                        UiController.AddMoveActionListener(Dispatcher);
                        break;
                    case UnitActionType.Attack:
                        UiController.AddAttackActionListener(Dispatcher);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}