using System;
using System.Collections.Generic;
using _Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create UnitSettingsConfig", fileName = "UnitSettingsConfig", order = 1)]
    public class UnitSettingsConfig : ScriptableObject
    {
        public UnitSettings[] UnitSettings;

        private void OnValidate()
        {
            foreach (var unitSetting in UnitSettings)
            {
                unitSetting.Name = unitSetting.UnitType.ToString();
            }
        }
    }

    [Serializable]
    public class UnitSettings
    {
        [HideInInspector] public string Name;
        public EntityTypes EntityType;
        public UnitTypes UnitType;
        public int MovementRange;
        public UnitActionTypes[] Actions;
        public int BaseHitPoints;
        public int BaseActionPoints;
        public int BaseSpellPoints;
    }
}