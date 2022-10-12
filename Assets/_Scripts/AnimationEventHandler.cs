using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts
{
    public class AnimationEventHandler : MonoBehaviour
    {
        [Inject] public IEventDispatcher Dispatcher { private get; set; }
        
        public void FootR()
        {
        }       
        
        public void FootL()
        {
        }

        public void Shoot()
        {
            Dispatcher.Dispatch(UnitEvents.SpellCastShoot);
        }

        public void SpellCastFinished()
        {
            Dispatcher.Dispatch(UnitEvents.SpellCastFinished);
        }
    }
}