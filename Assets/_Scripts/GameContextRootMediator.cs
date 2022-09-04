using System;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace _Scripts
{
    public class GameContextRootMediator : Mediator
    {
        public Signal OnUpdate = new Signal();

        private GameContextRoot _view;
        public void Start()
        {
            _view = contextView.GetComponent<GameContextRoot>();
        }

        public void Update()
        {
            OnUpdate.Dispatch();
        }

        public GameObject DebugMousePointer => _view.DebugMousePointer;

        public LayerMask MouseSelectionLayerMask => _view.MouseLayerMask;
    }
}