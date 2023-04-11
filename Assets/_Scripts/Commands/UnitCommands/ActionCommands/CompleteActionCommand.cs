using System;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class CompleteActionCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher EventDispatcher { private get; set; }
        public override void Execute()
        {
            Debug.Log($"{LogHelper.ActionTag} Complete:  {UnitModel.SelectedAction}");
            UiController.SetActionInProgressUi(false);
            UnitModel.SelectedAction = UnitActionType.None;
            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            EventDispatcher.Dispatch(UnitEvents.ActionEnded);
        }
    }
}