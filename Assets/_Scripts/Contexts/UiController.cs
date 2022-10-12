using System;
using _Scripts.Models;
using _Scripts.Views;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine.UI;

namespace _Scripts
{
    public class UiController
    {
        [Inject(ContextKeys.CROSS_CONTEXT_DISPATCHER)] public IEventDispatcher CrossContextDispatcher { private get; set; }
        
        private readonly MainUIContextRoot view;

        public UiController(MainUIContextRoot view)
        {
            this.view = view;
        }

        public ActionButtonView MoveActionButton => view.MoveActionButton;
        public ActionButtonView AttackActionButton => view.AttackActionButton;

        public void Initialize()
        {
            view.EndTurnButton.onClick.AddListener(() => CrossContextDispatcher.Dispatch(GameEvents.EndPlayerTurn));
            UnhighlightActions();
            RemoveAllListeners();
            ToggleUnitStats(false);
        }

        public void AddMoveActionListener(IEventDispatcher dispatcher)
        {
            SetupButton(MoveActionButton.Button, dispatcher, UiEvents.MoveActionSelected);
        }
        
        public void AddAttackActionListener(IEventDispatcher dispatcher)
        {
            SetupButton(AttackActionButton.Button, dispatcher, UiEvents.AttackActionSelected);
        }

        private void SetupButton(Button button, IEventDispatcher dispatcher, UiEvents unitActionType)
        {
            button.gameObject.SetActive(true);
            button.onClick.AddListener(() => dispatcher.Dispatch(unitActionType));
        }

        public void RemoveAllListeners()
        {
            DisableButton(MoveActionButton.Button);
            DisableButton(AttackActionButton.Button);
        }

        private void DisableButton(Button button)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }

        public void SetAllActionButtonsNotInteractable()
        {
            MoveActionButton.Button.interactable = false;
            AttackActionButton.Button.interactable = false;
        }

        public void SetActionButtonInteractable(UnitActionTypes action, bool isInteractable)
        {
            switch (action)
            {
                case UnitActionTypes.None:
                    break;
                case UnitActionTypes.Move:
                    MoveActionButton.Button.interactable = isInteractable;
                    break;
                case UnitActionTypes.Attack:
                    AttackActionButton.Button.interactable = isInteractable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        public void HighlightSelectedAction(UnitActionTypes action)
        {
            switch (action)
            {
                case UnitActionTypes.None:
                    UnhighlightActions();
                    break;
                case UnitActionTypes.Move:
                    EnableHighlightOnButton(MoveActionButton);
                    break;
                case UnitActionTypes.Attack:
                    EnableHighlightOnButton(AttackActionButton);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void EnableHighlightOnButton(ActionButtonView buttonView)
        {
            UnhighlightActions();
            buttonView.Highlight.gameObject.SetActive(true);
        }

        public void UnhighlightActions()
        {
            MoveActionButton.Highlight.gameObject.SetActive(false);
            AttackActionButton.Highlight.gameObject.SetActive(false);
        }

        public void UpdateUnitHitPoints(int currentHp, int maxHp)
        {
            view.UnitStatsView.HitPointsOfMax.text = $"{currentHp}/{maxHp}";
        }
        
        public void UpdateUnitActionPoints(int currentAp, int maxAp)
        {
            view.UnitStatsView.ActionPointsOfMax.text = $"{currentAp}/{maxAp}";
        }
        
        public void UpdateUnitSpellPoints(int currentSp, int maxSp)
        {
            view.UnitStatsView.SpellPointsOfMax.text = $"{currentSp}/{maxSp}";
        }
        
        public void ToggleUnitStats(bool shouldShow)
        {
            view.UnitStatsView.gameObject.SetActive(shouldShow);
        }

        public void ToggleActionsBar(bool shouldShow)
        {
            view.ActionsBarContainer.SetActive(shouldShow);
        }
        
        public void ToggleEndTurnButton(bool shouldShow)
        {
            view.EndTurnButton.gameObject.SetActive(shouldShow);
        }

        public void UpdateTurnNumber(int turnNumber)
        {
            view.TurnNumber.text = $"Turn: {turnNumber}";
        }
    }
}