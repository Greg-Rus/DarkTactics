using _Scripts.Helpers;
using _Scripts.Models;
using RPGCharacterAnims.Lookups;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleAttackSelectionCommand : EventCommand
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        public override void Execute()
        {
            UnitModel.SelectedAction = UnitActionType.Attack;
            new UpdateAttackRangeCellsCommand().InjectWith(injectionBinder).Execute();
        }
    }
}