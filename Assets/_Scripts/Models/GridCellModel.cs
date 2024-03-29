﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Models
{
    public class TileModel
    {
        public Vector2Int Coordinates;
        public readonly HashSet<int> Entities; 

        public TileModel(Vector2Int coordinates)
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