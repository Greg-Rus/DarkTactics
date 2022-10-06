using UnityEngine;

namespace _Scripts.EventPayloads
{
    public struct SpawnEventPayload
    {
        public int Id;
        public Vector2Int InitialPosition;
        public UnitSettings Settings;
    }
}