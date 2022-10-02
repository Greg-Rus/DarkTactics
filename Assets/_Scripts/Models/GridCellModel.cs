using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Models
{
    public class GridCellModel
    {
        public Vector2Int Coordinates;
        public readonly HashSet<int> Entities; 

        public GridCellModel(Vector2Int coordinates)
        {
            Coordinates = coordinates;
            Entities = new HashSet<int>();
        }

        public void SetEntityId(int entityId)
        {
            Entities.Add(entityId);
        }

        public bool TryRemoveEntityId(int entityId)
        {
            return Entities.Remove(entityId);
        }
    }
}