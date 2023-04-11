using System;
using UnityEngine;

namespace _Scripts.Views
{
    public class ProjectileView : MonoBehaviour
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        public Rigidbody Rigidbody;

        private DamageEffectConfig _damageEffect;


        public void Init(DamageEffectConfig damageEffect)
        {
            _damageEffect = damageEffect;
        }

        void OnCollisionEnter(Collision collision)
        {
            var hitUnit = EntityRegistryService.GetFasadeByTransform(collision.collider.transform);
            hitUnit.EventDispatcher.Dispatch(UnitEvents.HitTaken, _damageEffect);
            Destroy(gameObject);
        }
    }
}