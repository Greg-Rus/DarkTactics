
using UnityEngine;

namespace _Scripts.EventPayloads
{
    public struct MoveSelectedUnitPayload
    {
        public int UnitId;
        public Vector3 Destination;
    }
}