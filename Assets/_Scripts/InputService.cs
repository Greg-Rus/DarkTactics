using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts
{
    public class InputService
    {
        [Inject(ContextKeys.CROSS_CONTEXT_DISPATCHER)] public IEventDispatcher Dispatcher { private get; set; }
        [Inject] public GameContextRootMediator ContextMediator { private get; set; }
        private Camera _mainCamera;

        public void Initialize()
        {
            ContextMediator.OnUpdate.AddListener(OnUpdate);
            _mainCamera = Camera.main;
        }

        private void OnUpdate()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, ContextMediator.MouseSelectionLayerMask))
            {
                ContextMediator.DebugMousePointer.transform.position = hit.point;
            }
        }
    }
}