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

        public int GetActionCost(UnitActionType type)
        {
            return ActionSettings.Single(config => config.type == type).ActionPointCost;
        }
    }

    [Serializable]
    public class ActionConfig
    {
        public UnitActionType type;
        public int ActionPointCost;
    }
}