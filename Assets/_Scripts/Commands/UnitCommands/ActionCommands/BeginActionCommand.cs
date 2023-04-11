using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class BeginActionCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }

        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        
        public override void Execute()
        {
            Debug.Log($"{LogHelper.ActionTag} Begin:  {UnitModel.SelectedAction}");
            GridVisualsService.ClearGrid();
            UnitModel.ActionRangeCells = new GridCellModel[0,0];
            UiController.SetActionInProgressUi(true);
        }
    }
}