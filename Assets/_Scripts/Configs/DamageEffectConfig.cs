using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(menuName = "Create DamageEffectConfig", fileName = "DamageEffectConfig", order = 2)]
    public class DamageEffectConfig : ScriptableObject
    {
        public int Damage;
    }
}