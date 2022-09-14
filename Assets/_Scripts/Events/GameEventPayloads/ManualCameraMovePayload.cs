using UnityEngine;

namespace _Scripts.EventPayloads
{
    public struct ManualCameraMovePayload
    {
        public Vector3? Translation;
        public float? RotationDirection;
        public float? ZoomDirection;
    }
}