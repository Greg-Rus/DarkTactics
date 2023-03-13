using _Scripts.Controllers;
using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Commands.UnitCommands
{
    public class FilterAttackActionCommand : EventCommand<AttackActionPayload>
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public UnitSensor UnitSensor { private get; set; }
        public override void Execute()
        {
            if (IsSelectedActionAttack &&
                IsAlreadyAttacking == false &&
                HasEnoughActionPointsToAttack &&
                IsTargetInRange)
            {
                return;
            }
            Fail();
        }

        private bool IsSelectedActionAttack => UnitModel.SelectedAction == UnitActionTypes.Attack;
        private bool IsAlreadyAttacking => UnitModel.IsAttacking;
        private bool HasEnoughActionPointsToAttack => UnitStateController.CanPerformAction(UnitActionTypes.Attack);
        private bool IsTargetInRange => UnitSensor.IsCellInRange(Payload.TargetCoordinates);
    }
}