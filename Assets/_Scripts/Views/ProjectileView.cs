using System;
using UnityEngine;

namespace _Scripts.Views
{
    public class ProjectileView : MonoBehaviour
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        public Rigidbody Rigidbody;

        private DamageEffectConfig _damageEffect;

        private void Awake()
        {
            Debug.Log("Projectile awake");
        }

        public void Init(DamageEffectConfig damageEffect)
        {
            _damageEffect = damageEffect;
        }

        void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Touch start");
            var hitUnit = EntityRegistryService.GetFasadeByTransform(collision.collider.transform);
            hitUnit.EventDispatcher.Dispatch(UnitEvents.HitTaken, _damageEffect);
            Destroy(gameObject);
        }
    }
}