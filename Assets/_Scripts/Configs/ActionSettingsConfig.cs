using System;
using System.Linq;
using _Scripts.Models;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create ActionSettingsConfig", fileName = "ActionSettingsConfig", order = 1)]
    public class ActionSettingsConfig : ScriptableObject
    {
        public ActionConfig[] ActionSettings;

        public int GetActionCost(UnitActionTypes type)
        {
            return ActionSettings.Single(config => config.Type == type).ActionPointCost;
        }
    }

    [Serializable]
    public class ActionConfig
    {
        public UnitActionTypes Type;
        public int ActionPointCost;
    }
}