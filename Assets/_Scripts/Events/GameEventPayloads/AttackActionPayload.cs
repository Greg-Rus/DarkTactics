using UnityEngine;
using UnityEngine.Rendering;

namespace _Scripts.EventPayloads
{
    public struct AttackActionPayload
    {
        public Transform TargetTransform;
        public int TargetId;
        public Vector2 TargetCoordinates;
    }
}