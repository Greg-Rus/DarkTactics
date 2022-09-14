using _Scripts.Views;
using Cinemachine;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class GameContextRoot : ContextView
    {
        [Header("Prefabs")]
        public PrefabConfig PrefabConfig;
        public DebugGridCellView DebugGridCellMarker;
        public GameObject DebugMousePointer;
        
        [Header("Physics Layers")]
        public LayerMask GroundLayerMask;
        public LayerMask UnitLayerMask;
        
        [Header("Parents")]
        public Transform DebugRoot;
        public Transform UnitsRoot;

        [Header("Camera")] 
        public Transform CameraTarget;

        public CinemachineVirtualCamera VirtualCamera;
    }
}