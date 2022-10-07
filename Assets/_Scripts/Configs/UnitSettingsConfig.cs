using System;
using System.Collections.Generic;
using _Scripts.Models;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create UnitSettingsConfig", fileName = "UnitSettingsConfig", order = 1)]
    public class UnitSettingsConfig : ScriptableObject
    {
        public UnitSettings[] UnitSettings;
    }

    [Serializable]
    public class UnitSettings
    {
        public UnityTypes Type;
        public int MovementRange;
        public UnitActionTypes[] Actions;
        public int BaseHitPoints;
        public int BaseActionPoints;
        public int BaseSpellPoints;
    }
}