using System;
using UnityEngine;

namespace _Scripts.Views
{
    public class ProjectileView : MonoBehaviour
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        public Rigidbody Rigidbody;

        private DamageEffectConfig _damageEffect;


        public void Init(DamageEffectConfig damageEffect, int projectileLayer)
        {
            _damageEffect = damageEffect;
            gameObject.layer = projectileLayer;
        }

        void OnCollisionEnter(Collision collision)
        {
            var hitUnit = EntityRegistryService.TryGetFasadeByTransform(collision.collider.transform);
            if (hitUnit == null) return;
            
            hitUnit.EventDispatcher.Dispatch(UnitEvents.HitTaken, _damageEffect);
            Destroy(gameObject);
        }
    }
}