﻿using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleMoveActionSelectedCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }

        public override void Execute()
        {
            if (UnitStateController.CanPerformAction(UnitActionType.Move))
            {
                UnitModel.SelectedAction = UnitActionType.Move;
                new UpdateWalkableTilesCommand().InjectWith(injectionBinder).Execute();
            }
            else
            {
                Cancel();
            }
        }
    }
}