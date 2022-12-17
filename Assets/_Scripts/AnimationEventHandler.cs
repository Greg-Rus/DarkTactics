using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts
{
    public class AnimationEventHandler : MonoBehaviour
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { private get; set; }
        
        public void FootR()
        {
        }       
        
        public void FootL()
        {
        }

        public void AttackEmit()
        {
            Dispatcher.Dispatch(AnimationEvents.AttackEmit);
        }

        public void AttackFinished()
        {
            Dispatcher.Dispatch(AnimationEvents.AttackFinished);
        }
    }
}