using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Models
{
    public class GridCellModel
    {
        public Vector2Int Coordinates;
        public readonly List<EntityData> Entities; 

        public GridCellModel(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            Entities = new List<EntityData>();
        }

        public void SetEntityId(int entityId, EntityTypes type)
        {
            Entities.Add(new EntityData(){EntityId = entityId, EntityType = type});
        }

        public bool TryRemoveEntityId(int entityId)
        {
            var index = Entities.FindIndex(data => data.EntityId == entityId);
            if (index != -1)
            {
                Entities.RemoveAt(index);
                return true;
            }

            return false;
        }
    }
}