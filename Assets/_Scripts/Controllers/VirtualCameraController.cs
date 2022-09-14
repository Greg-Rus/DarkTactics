using Cinemachine;
using UnityEngine;

namespace _Scripts.Controllers
{
    public class VirtualCameraController
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly Transform _target;
        private CinemachineTransposer _transposer;
        public VirtualCameraController(CinemachineVirtualCamera camera, Transform target)
        {
            _camera = camera;
            _target = target;
            _transposer = _camera.GetCinemachineComponent<CinemachineTransposer>();
        }

        public void SetTargetPosition(Vector3 position)
        {
            _target.position = position;
        }

        public void SetTargetRotation(float angle)
        {
            _target.localRotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
        }

        public void SetZoom(float zoom)
        {
            _transposer.m_FollowOffset = new Vector3(0f, zoom, 0f);
        }
    }
}