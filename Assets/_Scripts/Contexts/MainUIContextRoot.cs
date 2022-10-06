using System;
using strange.extensions.context.impl;
using UnityEngine.UI;

namespace _Scripts
{
    public class MainUIContextRoot : ContextView
    {
        public Button MoveActionButton;
        public Button AttackActionButton;

        private void Awake()
        {
            context = new MainUiContext(this, true);
        }
    }
}