
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts.Models
{
    public class GridCellModel
    {
        private Vector2 _position;
        private readonly HashSet<int> _entityIds; 

        public GridCellModel(Vector2 position)
        {
            _position = position;
            _entityIds = new HashSet<int>();
        }

        public void SetEntityId(int entityId)
        {
            _entityIds.Add(entityId);
        }

        public bool TryRemoveEntityId(int entityId)
        {
            if (_entityIds.Contains(entityId))
            {
                _entityIds.Remove(entityId);
                return true;
            }

            return false;
        }
    }
}