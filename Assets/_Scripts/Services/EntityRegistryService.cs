using System;
using System.Collections.Generic;
using _Scripts.Models;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class EntityRegistryService
    {
        private readonly Dictionary<int, EntityFasade> _entityIdToFasadeDictionary;
        private readonly Dictionary<Transform, int> _transformToEntityDictionary;
        private readonly HashSet<int> _playerUnitIds;
        private readonly HashSet<int> _enemyUnitIds;
        private int nextEntityId = 1;

        public EntityRegistryService()
        {
            _entityIdToFasadeDictionary = new Dictionary<int, EntityFasade>();
            _transformToEntityDictionary = new Dictionary<Transform, int>();
            _playerUnitIds = new HashSet<int>();
            _enemyUnitIds = new HashSet<int>();
        }

        public void RegisterEntityFasade(EntityFasade fasade)
        {
            _entityIdToFasadeDictionary.Add(fasade.EntityModel.Id, fasade);
            _transformToEntityDictionary.Add(fasade.Transform, fasade.EntityModel.Id);
            switch (fasade.EntityModel.EntityType)
            {
                case EntityType.PlayerUnit:
                    _playerUnitIds.Add(fasade.EntityModel.Id);
                    break;
                case EntityType.EnemyUnit:
                    _enemyUnitIds.Add(fasade.EntityModel.Id);
                    break;
            }
        }

        public void DeregisterEntityFasade(EntityFasade fasade)
        {
            _entityIdToFasadeDictionary.Remove(fasade.EntityModel.Id);
            _transformToEntityDictionary.Remove(fasade.Transform);
            switch (fasade.EntityModel.EntityType)
            {
                case EntityType.PlayerUnit:
                    _playerUnitIds.Remove(fasade.EntityModel.Id);
                    break;
                case EntityType.EnemyUnit:
                    _enemyUnitIds.Remove(fasade.EntityModel.Id);
                    break;
            }
        }

        public EntityFasade GetFasadeById(int unitId)
        {
            return _entityIdToFasadeDictionary[unitId];
        }
        
        public int? TryGetEntityIdByTransform(Transform transform)
        {
            if(_transformToEntityDictionary.TryGetValue(transform, out var id))
            {
                return id;
            }

            return null;
        }

        public Transform GetTransformByEntityId(int id)
        {
            return GetFasadeById(id).Transform;
        }
        
        public EntityFasade TryGetFasadeByTransform(Transform transform)
        {
            var id = TryGetEntityIdByTransform(transform);
            return id.HasValue ? _entityIdToFasadeDictionary[id.Value] : null;
        }

        public IEnumerable<int> GetAllEntityIds()
        {
            return _entityIdToFasadeDictionary.Keys;
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