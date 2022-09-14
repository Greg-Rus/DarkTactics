using _Scripts.Views;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create PrefabConfig", fileName = "PrefabConfig", order = 0)]
    public class PrefabConfig : ScriptableObject
    {
        public UnitContextRoot Unit;
    }
}