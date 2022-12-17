using System;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class UnitContextRoot : ContextView
    {
        public Animator Animator;
        public GameObject SelectionMarker;
        public AnimationEventHandler AnimationEventHandler;
        public Transform RightHandSpawnPoint;
    }
}