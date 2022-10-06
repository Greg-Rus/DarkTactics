using System;
using _Scripts.Models;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class MainUiController
    {
        private readonly MainUIContextRoot view;

        public MainUiController(MainUIContextRoot view)
        {
            this.view = view;
        }

        public Button MoveActionButton => view.MoveActionButton;
        public Button AttackActionButton => view.AttackActionButton;

        public void AddMoveActionListener(IEventDispatcher dispatcher)
        {
            SetupButton(MoveActionButton, dispatcher, UnitEvents.MoveActionSelected);
        }
        
        public void AddAttackActionListener(IEventDispatcher dispatcher)
        {
            SetupButton(AttackActionButton, dispatcher, UnitEvents.AttackActionSelected);
        }

        private void SetupButton(Button button, IEventDispatcher dispatcher, UnitEvents unitActionType)
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => dispatcher.Dispatch(unitActionType));
        }

        public void RemoveAllListeners()
        {
            DisableButton(MoveActionButton);
            DisableButton(AttackActionButton);
        }

        private void DisableButton(Button button)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }
}