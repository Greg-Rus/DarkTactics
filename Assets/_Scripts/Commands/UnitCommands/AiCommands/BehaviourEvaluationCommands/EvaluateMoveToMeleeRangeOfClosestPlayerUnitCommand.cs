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

            var tilesInMoveRange = GridService.GetTileCoordinatesInRange(UnitModel.Settings.MovementRange,
                UnitModel.OccupiedTileModel.Coordinates);
            var destinationTile =
                GridService.GetClosestTileInRangeTowardsTarget(tilesInMoveRange, closestUnit.Position);

            return new AiAction()
            {
                Score = _behaviour.Score,
                ActionType = UnitActionType.Move,
                TargetTileCoordinates = destinationTile,
                TargetUnitId = closestUnit.EntityId
            };
        }
    }
}