using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Models
{
    public class GridCellModel
    {
        private Vector2 _position;
        private readonly List<EntityData> _entityIds; 

        public GridCellModel(Vector2 position)
        {
            _position = position;
            _entityIds = new List<EntityData>();
        }

        public void SetEntityId(int entityId, EntityTypes type)
        {
            _entityIds.Add(new EntityData(){EntityId = entityId, EntityType = type});
        }

        public bool TryRemoveEntityId(int entityId)
        {
            var index = _entityIds.FindIndex(data => data.EntityId == entityId);
            if (index != -1)
            {
                _entityIds.RemoveAt(index);
                return true;
            }

            return false;
        }
    }
}