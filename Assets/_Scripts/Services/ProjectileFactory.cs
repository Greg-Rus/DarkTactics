using _Scripts.Views;
using strange.extensions.injector.api;
using UnityEngine;

namespace _Scripts
{
    public class ProjectileFactory
    {
        [Inject] public PrefabConfig PrefabConfig { private get; set; }
        [Inject] public IInjectionBinder InjectionBinder { private get; set; }

        public ProjectileView SpawnProjectile()
        {
            var view = GameObject.Instantiate(PrefabConfig.Projectile);
            InjectionBinder.injector.Inject(view);
            return view;
        }
    }
}