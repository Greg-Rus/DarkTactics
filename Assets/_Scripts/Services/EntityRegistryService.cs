using System;
using System.Collections.Generic;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class EntityRegistryService
    {
        private readonly Dictionary<int, MVCSContext> _entityIdToContextDictionary;
        private readonly Dictionary<Transform, int> _transformToEntityDictionary;
        private readonly HashSet<int> _playerUnitIds;
        private readonly HashSet<int> _enemyUnitIds;
        private int nextEntityId = 0;

        public EntityRegistryService()
        {
            _entityIdToContextDictionary = new Dictionary<int, MVCSContext>();
            _transformToEntityDictionary = new Dictionary<Transform, int>();
            _playerUnitIds = new HashSet<int>();
            _enemyUnitIds = new HashSet<int>();
        }

        public void RegisterEntity(int entityId, MVCSContext unitContext, Transform transform, EntityTypes type)
        {
            _entityIdToContextDictionary.Add(entityId, unitContext);
            _transformToEntityDictionary.Add(transform, entityId);
            switch (type)
            {
                case EntityTypes.PlayerUnit:
                    _playerUnitIds.Add(entityId);
                    break;
                case EntityTypes.EnemyUnit:
                    _enemyUnitIds.Add(entityId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public MVCSContext GetEntityContextById(int unitId)
        {
            return _entityIdToContextDictionary[unitId];
        }
        
        public int GetEntityIdByTransform(Transform transform)
        {
            return _transformToEntityDictionary[transform];
        }
        
        public MVCSContext GetEntityContextByTransform(Transform transform)
        {
            return GetEntityContextById(GetEntityIdByTransform(transform));
        }

        public IEnumerable<int> GetAllEntityIds()
        {
            return _entityIdToContextDictionary.Keys;
        }
        
        public IEnumerable<int> GetAllPlayerUnitId()
        {
            return _playerUnitIds;
        }
        
        public IEnumerable<int> GetAllEnemyIds()
        {
            return _enemyUnitIds;
        }

        public int NextEntityId => nextEntityId++;

    }
}