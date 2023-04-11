using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class EvaluateRangedAttackClosestCommand : ReturnCommand<AiAction>
    {
        private readonly AiBehaviour _aiBehaviour;
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }

        public EvaluateRangedAttackClosestCommand(AiBehaviour aiBehaviour)
        {
            _aiBehaviour = aiBehaviour;
        }

        public override AiAction Execute()
        {
            var closestUnit = new GetClosestPlayerUnitIdCommand(EntityRegistryService.GetAllPlayerUnitId())
                .InjectWith(injectionBinder)
                .Execute();

            if (closestUnit.Distance <= UnitModel.Settings.AttackRange)
            {
                return new AiAction()
                {
                    Score = _aiBehaviour.Score,
                    ActionType = UnitActionType.Attack,
                    TargetUnitId = closestUnit.EntityId,
                    TargetGridCellCoordinates = closestUnit.Position
                };
            }
            
            return new AiAction()
            {
                Score = 0,
                ActionType = UnitActionType.Attack
            };
        }
    }
}