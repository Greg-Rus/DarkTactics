using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class HandleAttackSelectionCommand : EventCommand
    {
        public override void Execute()
        {
            Debug.Log($"Starting attack sequence");
        }
    }
}