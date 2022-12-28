using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class CompleteActionCommand : Command
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        public override void Execute()
        {
            Debug.Log("Complete Action");
            UiController.SetActionInProgressUi(false);
            UnitModel.SelectedAction = UnitActionTypes.None;
            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
        }
    }
}