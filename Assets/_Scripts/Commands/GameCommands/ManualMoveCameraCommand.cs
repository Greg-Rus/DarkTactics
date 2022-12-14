using _Scripts.EventPayloads;
using Cinemachine;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class ManualMoveCameraCommand : EventCommand
    {
        [Inject] public GameContextRoot GameContextRoot { get; set; }
        private const float K_TranslationSpeed = 10;
        private const float K_RotationSpeed = 75;

        public override void Execute()
        {
            var payload = (ManualCameraMovePayload)evt.data;

            if (payload.Translation.HasValue)
            {
                var newPosition = GameContextRoot.CameraTarget.forward * payload.Translation.Value.z +
                                  GameContextRoot.CameraTarget.right * payload.Translation.Value.x;
                GameContextRoot.CameraTarget.position += newPosition * K_TranslationSpeed * Time.deltaTime;
            }

            if (payload.RotationDirection.HasValue)
            {
                var currentRotation = GameContextRoot.CameraTarget.localRotation.eulerAngles.y;
                            var newRotation = Mathf.LerpAngle(currentRotation,
                                currentRotation + payload.RotationDirection.Value * K_RotationSpeed * Time.deltaTime, 1);
                            GameContextRoot.CameraTarget.localRotation = Quaternion.Euler(0f, newRotation, 0f);
            }

            if (payload.ZoomDirection.HasValue)
            {
                var transposer = GameContextRoot.VirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
                var currentZoom = transposer.m_FollowOffset;
                var zoomAmount = currentZoom.y;
                var newZoom = Mathf.Clamp(zoomAmount + payload.ZoomDirection.Value * -1f, 4, 15);
                transposer.m_FollowOffset = new Vector3(currentZoom.x, newZoom, currentZoom.z);
            }
        }
    }
}