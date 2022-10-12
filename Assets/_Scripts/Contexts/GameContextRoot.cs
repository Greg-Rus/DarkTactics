using _Scripts.Views;
using Cinemachine;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class GameContextRoot : ContextView
    {
        private void Awake()
        {
            context = new GameContext(this, true);
            context.Start();
        }

        [Header("Prefabs")]
        public DebugGridCellView DebugGridCellMarker;
        public GameObject DebugMousePointer;
        public GridVisualView GridVisualView;
        
        [Header("Physics Layers")]
        public LayerMask GroundLayerMask;
        public LayerMask UnitLayerMask;
        public LayerMask EnemyLayerMask;
        
        [Header("Parents")]
        public Transform DebugRoot;
        public Transform UnitsRoot;
        public Transform GridVisualsRoot;

        [Header("Camera")] 
        public Transform CameraTarget;

        public CinemachineVirtualCamera VirtualCamera;
    }
}