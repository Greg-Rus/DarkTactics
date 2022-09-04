using System;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class GameContextRoot : ContextView
    {
        public PrefabConfig PrefabConfig;
        public GameObject DebugMousePointer;
        public LayerMask MouseLayerMask;
    }
}