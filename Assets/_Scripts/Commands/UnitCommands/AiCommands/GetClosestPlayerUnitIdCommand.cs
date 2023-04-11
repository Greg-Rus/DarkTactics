using System.Collections.Generic;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands.AiCommands
{
    public class GetClosestPlayerUnitIdCommand : ReturnCommand<ClosestEntityResult>
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        [Inject] public UnitModel UnitModel { private get; set; }

        private IEnumerable<int> _entityIDs;

        public GetClosestPlayerUnitIdCommand(IEnumerable<int> entityIDs)
        {
            _entityIDs = entityIDs;
        }

        public override ClosestEntityResult Execute()
        {
            var result = new ClosestEntityResult()
            {
                EntityId = 0,
                Distance = float.MaxValue,
            };

            foreach (var id in _entityIDs)
            {
                var playerUnitPosition = EntityRegistryService.GetFasadeById(id);

                var otherEntityPosition = playerUnitPosition.EntityModel.OccupiedCellModel.Coordinates;
                var thisEntityPosition = UnitModel.OccupiedCellModel.Coordinates;
                var distance = (thisEntityPosition - otherEntityPosition).magnitude;

                if (distance < result.Distance)
                {
                    result.Distance = distance;
                    result.EntityId = id;
                    result.Position = otherEntityPosition;
                }
            }

            return result;
        }
    }
    
    public struct ClosestEntityResult
    {
        public int EntityId;
        public float Distance;
        public Vector2Int Position;
    }
}