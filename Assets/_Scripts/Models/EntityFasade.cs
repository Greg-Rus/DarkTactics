using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Models
{
    public class EntityFasade
    {
        [Inject] public EntityModel EntityModel {  get; set; }
        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher EventDispatcher { get; set; }
        [Inject] public UnitContextRoot UnitContextRoot {  get; set; }
        public Transform Transform => UnitContextRoot.transform;
    }
}