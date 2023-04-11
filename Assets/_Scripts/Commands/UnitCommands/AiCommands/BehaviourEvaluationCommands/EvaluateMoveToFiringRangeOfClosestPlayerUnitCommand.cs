using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class EvaluateMoveToFiringRangeOfClosestPlayerUnitCommand : ReturnCommand<AiAction>
    {
        private readonly AiBehaviour _aiBehaviour;
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        [Inject] public GridService GridService { private get; set; }

        [Inject] public UnitModel UnitModel { private get; set; }

        public EvaluateMoveToFiringRangeOfClosestPlayerUnitCommand(AiBehaviour aiBehaviour)
        {
            _aiBehaviour = aiBehaviour;
        }

        public override AiAction Execute()
        {
            var closestUnit = new GetClosestPlayerUnitIdCommand(EntityRegistryService.GetAllPlayerUnitId())
                .InjectWith(injectionBinder)
                .Execute();
            
            var cellsInAttackRange = GridService.GetCellCoordinatesInRange(UnitModel.Settings.AttackRange,
                closestUnit.Position);
            var destinationCell =
                GridService.GetClosestCellInRangeTowardsTarget(cellsInAttackRange, UnitModel.OccupiedCellModel.Coordinates);

            return new AiAction()
            {
                Score = _aiBehaviour.Score,
                ActionType = UnitActionType.Move,
                TargetGridCellCoordinates = destinationCell,
                TargetUnitId = closestUnit.EntityId
            };
        }
    }
}