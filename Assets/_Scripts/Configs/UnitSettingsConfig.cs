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
        public EntityType EntityType;
        public UnitType UnitType;
        public int MovementRange;
        public int AttackRange;
        public DamageEffectConfig AttackDamageEffect;
        public UnitActionType[] Actions;
        public AiBehaviour[] AiBehaviours;
        public int BaseHitPoints;
        public int BaseActionPoints;
        public int BaseSpellPoints;
    }

    [Serializable]
    public class AiBehaviour
    {
        public AiBehaviourType Behaviour;
        public int Score;
    }
}