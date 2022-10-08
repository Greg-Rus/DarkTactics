using System;
using _Scripts.Views;
using strange.extensions.context.impl;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class MainUIContextRoot : ContextView
    {
        [Header("Bottom Hud")]
        public ActionButtonView MoveActionButton;
        public ActionButtonView AttackActionButton;
        public UnitStatsView UnitStatsView;
        
        [Header("Top Hud")]
        public Button EndTurnButton;
        public TextMeshProUGUI TurnNumber;

        private void Awake()
        {
            context = new MainUiContext(this, true);
        }
    }
}