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
        public GridVisualView GridVisualView;
        
        [Header("Physics Layers")]
        public LayerMask GroundLayerMask;
        public LayerMask UnitLayerMask;
        
        [Header("Parents")]
        public Transform DebugRoot;
        public Transform UnitsRoot;
        public Transform GridVisualsRoot;

        [Header("Camera")] 
        public Transform CameraTarget;

        public CinemachineVirtualCamera VirtualCamera;
    }
}