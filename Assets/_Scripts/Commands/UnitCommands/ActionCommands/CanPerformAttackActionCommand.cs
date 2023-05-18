using _Scripts.Controllers;
using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class CanPerformAttackActionCommand : EventCommand<AttackActionPayload>
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public UnitSensor UnitSensor { private get; set; }
        public override void Execute()
        {
            if (UnitStateController.IsAlive &&
                IsSelectedActionAttack &&
                IsAlreadyAttacking == false &&
                HasEnoughActionPointsToAttack &&
                IsTargetInRange)
            {
                return;
            }
            Fail();
        }

        private bool IsSelectedActionAttack => UnitModel.SelectedAction == UnitActionType.Attack;
        private bool IsAlreadyAttacking => UnitModel.IsAttacking;
        private bool HasEnoughActionPointsToAttack => UnitStateController.CanPerformAction(UnitActionType.Attack);
        private bool IsTargetInRange => UnitSensor.IsTileInRange(Payload.TargetCoordinates);
    }
}