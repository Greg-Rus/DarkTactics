using _Scripts.EventPayloads;
using _Scripts.Models;
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
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit hitUnit, float.PositiveInfinity, ContextMediator.UnitLayerMask))
                {
                    Dispatcher.Dispatch(GameEvent.MouseClickUnit,  new MouseClickUnitPayload { UnitTransform = hitUnit.transform});
                    return;
                }
                
                if (Physics.Raycast(ray, out RaycastHit hitEnemy, float.PositiveInfinity, ContextMediator.EnemyLayerMask))
                {
                    Dispatcher.Dispatch(GameEvent.MouseClickEnemy,  new AttackActionPayload() { TargetTransform = hitEnemy.collider.transform});
                    return;
                }

                if (Physics.Raycast(ray, out RaycastHit hitGround, float.PositiveInfinity, ContextMediator.GroundLayerMask))
                {
                    ContextMediator.DebugMousePointer.transform.position = hitGround.point;
                    Dispatcher.Dispatch(GameEvent.MouseClickGround,  new MouseClickGroundPayload { ClickPosition = hitGround.point});
                }
            }
            
            Vector3 cameraMoveOffset = Vector3.zero;
            float rotationDirection = 0f;
            float zoomDirection = 0f;
            if (Input.GetKey(KeyCode.W))
            {
                cameraMoveOffset += Vector3.forward;
            }
            
            if (Input.GetKey(KeyCode.S))
            {
                cameraMoveOffset += Vector3.back;
            }
            
            if (Input.GetKey(KeyCode.A))
            {
                cameraMoveOffset += Vector3.left;
            }
            
            if (Input.GetKey(KeyCode.D))
            {
                cameraMoveOffset += Vector3.right;
            }
            
            if (Input.GetKey(KeyCode.Q))
            {
                rotationDirection += -1f;
            }
            
            if (Input.GetKey(KeyCode.E))
            {
                rotationDirection += 1f;
            }
            
            if (Input.mouseScrollDelta != Vector2.zero)
            {
                zoomDirection = Input.mouseScrollDelta.y;
            }

            if (cameraMoveOffset != Vector3.zero || rotationDirection != 0 || zoomDirection != 0)
            {
                Dispatcher.Dispatch(GameEvent.ManualCameraMove, new ManualCameraMovePayload
                {
                    Translation = cameraMoveOffset,
                    RotationDirection = rotationDirection,
                    ZoomDirection = zoomDirection
                });
            }
        }
    }
}