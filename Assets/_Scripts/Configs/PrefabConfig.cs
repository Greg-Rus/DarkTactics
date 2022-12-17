using System;
using System.Linq;
using _Scripts.Models;
using _Scripts.Views;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create PrefabConfig", fileName = "PrefabConfig", order = 0)]
    public class PrefabConfig : ScriptableObject
    {
        public UnitPrefabMapping[] UnitPrefabMap;
        public ProjectileView Projectile;

        public UnitContextRoot GetPrefabForUnitType(UnitTypes type)
        {
            return UnitPrefabMap.Single(mapping => mapping.UnitTypes.Contains(type)).UnitPrefab;
        }
    }

    [Serializable]
    public class UnitPrefabMapping
    {
        public UnitTypes[] UnitTypes;
        public UnitContextRoot UnitPrefab;
    }
}