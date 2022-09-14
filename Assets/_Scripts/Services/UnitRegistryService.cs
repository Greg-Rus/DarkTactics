using System.Collections.Generic;
using System.Linq;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class UnitRegistryService
    {
        private readonly Dictionary<int, MVCSContext> _unitIdToContextDictionary;
        private readonly Dictionary<Transform, int> _transformToUnitDictionary;

        public UnitRegistryService()
        {
            _unitIdToContextDictionary = new Dictionary<int, MVCSContext>();
            _transformToUnitDictionary = new Dictionary<Transform, int>();
            
        }

        public void RegisterUnit(int unitId, MVCSContext unitContext, Transform transform)
        {
            _unitIdToContextDictionary.Add(unitId, unitContext);
            _transformToUnitDictionary.Add(transform, unitId);
        }

        public MVCSContext GetUnitContextById(int unitId)
        {
            return _unitIdToContextDictionary[unitId];
        }
        
        public int GetUnitIdByTransform(Transform transform)
        {
            return _transformToUnitDictionary[transform];
        }

        public IEnumerable<int> GetAllUnitIds()
        {
            return _unitIdToContextDictionary.Keys;
        }
    }
}