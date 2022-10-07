using System;
using _Scripts.Views;
using strange.extensions.context.impl;
using UnityEngine.UI;

namespace _Scripts
{
    public class MainUIContextRoot : ContextView
    {
        public ActionButtonView MoveActionButton;
        public ActionButtonView AttackActionButton;
        public Button EndTurnButton;
        public UnitStatsView UnitStatsView;

        private void Awake()
        {
            context = new MainUiContext(this, true);
        }
    }
}