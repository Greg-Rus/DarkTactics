using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class BeginActionCommand : Command
    {
        [Inject] public UiController UiController { private get; set; }

        [Inject] public GridVisualsService GridVisualsService { private get; set; }
        
        public override void Execute()
        {
            Debug.Log("Begin Action");
            GridVisualsService.ClearGrid();
            UiController.SetActionInProgressUi(true);
        }
    }
}