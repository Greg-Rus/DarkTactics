using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class EvaluateMoveToMeleeRangeOfClosestPlayerUnitCommand : ReturnCommand<AiAction>
    {
        private readonly AiBehaviour _behaviour;
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        [Inject] public GridService GridService { private get; set; }

        [Inject] public UnitModel UnitModel { private get; set; }

        public EvaluateMoveToMeleeRangeOfClosestPlayerUnitCommand(AiBehaviour behaviour)
        {
            _behaviour = behaviour;
        }

        public override AiAction Execute()
        {
            var closestUnit = new GetClosestPlayerUnitIdCommand(EntityRegistryService.GetAllPlayerUnitId())
                .InjectWith(injectionBinder)
                .Execute();

            var cellsInMoveRange = GridService.GetCellCoordinatesInRange(UnitModel.Settings.MovementRange,
                UnitModel.OccupiedCellModel.Coordinates);
            var destinationCell =
                GridService.GetClosestCellInRangeTowardsTarget(cellsInMoveRange, closestUnit.Position);

            return new AiAction()
            {
                Score = _behaviour.Score,
                ActionType = UnitActionType.Move,
                TargetGridCellCoordinates = destinationCell,
                TargetUnitId = closestUnit.EntityId
            };
        }
    }
}