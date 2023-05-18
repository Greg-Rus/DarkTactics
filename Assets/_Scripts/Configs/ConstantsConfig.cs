using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create ConstantsConfig", fileName = "ConstantsConfig", order = 3)]
    public class ConstantsConfig : ScriptableObject
    {
        public int PlayerProjectileLayer;
        public int EnemyProjectileLayer;
    }
}