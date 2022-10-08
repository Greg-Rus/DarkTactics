using System.Collections;
using _Scripts.Commands.UnitCommands;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class PerformMoveActionCommand : EventCommand
    {
        [Inject]  public UnitContextRoot RootView {get;set;}
        [Inject] public GridService GridService {get;set;}
        [Inject] public GridVisualsService GridVisualsService {get;set;}
        [Inject] public UnitModel Model {get;set;}
        [Inject] public UiController UiController { private get; set; }

        private const float K_RunningSpeed = 5f;
        private const float K_TurningSpeed = 5f;

        public override void Execute()
        {
            if (Model.SelectedAction != UnitActionTypes.Move) return;
            
            Debug.Log("Starting coroutine");
            RootView.StopAllCoroutines();
            var destinationGridCellModel = (GridCellModel)evt.data;

            if (IsCellWalkable(destinationGridCellModel) && IsCellInRange(destinationGridCellModel))
            {
                var cellWorldPosition = GridService.GridCoordinateToWorldPosition(destinationGridCellModel.Coordinates);
                RootView.StartCoroutine(MoveToPosition(cellWorldPosition));
                RootView.StartCoroutine(RotateToPosition(cellWorldPosition));
            }
        }

        private bool IsCellWalkable(GridCellModel cell)
        {
            return cell.Entities.Count == 0;
        }

        private bool IsCellInRange(GridCellModel cell)
        {
            var isWalkable = false;
            foreach (var cellModel in Model.WalkableCells)
            {
                if(cellModel == null) continue;
                if (cellModel.Coordinates == cell.Coordinates)
                {
                    isWalkable = true;
                    break;
                }
            }

            return isWalkable;
        }

        private IEnumerator MoveToPosition(Vector3 destination)
        {
            OnDeparture();
            
            bool moving = true;
            RootView.Animator.SetBool(AnimatorParameters.IsRunning, true);
            while (moving)
            {
                var directionToDestination = destination - RootView.transform.position;
                var nextTranslationStep = directionToDestination.normalized * Time.deltaTime * K_RunningSpeed;
                if (directionToDestination.sqrMagnitude <= nextTranslationStep.sqrMagnitude)
                {
                    RootView.transform.position = destination;
                    moving = false;
                }
                else
                {
                    RootView.transform.Translate(nextTranslationStep, Space.World);
                    yield return null;
                }
            }
            
            OnArrival(destination);
        }

        private IEnumerator RotateToPosition(Vector3 destination)
        {
            var startRotation = RootView.transform.localRotation;
            var directionToDestination = destination - RootView.transform.position;
            float angle = Mathf.Atan2(directionToDestination.x, directionToDestination.z) * Mathf.Rad2Deg;
            var targetRotation = Quaternion.Euler(0f, angle, 0f);
            
            float time = 0;
             
            while (time < 1)
            {
                time += Time.deltaTime * K_TurningSpeed;
                var lerpAngle = Quaternion.Lerp(startRotation, targetRotation, time);
                RootView.transform.localRotation = lerpAngle;
                yield return null;
            }
            RootView.transform.localRotation = targetRotation;
        }

        private void OnDeparture()
        {
            Model.OccupiedCellModel.Entities.Remove(Model.Id);
            GridVisualsService.ClearWalkableGrid();
            Model.SelectedAction = UnitActionTypes.None;
            UiController.MoveActionButton.Highlight.gameObject.SetActive(false);
            UiController.SetAllActionButtonsNotInteractable();
        }

        private void OnArrival(Vector3 destination)
        {
            RootView.Animator.SetBool(AnimatorParameters.IsRunning, false);
            Model.OccupiedCellModel = GridService.WorldPositionToGridCellModel(destination);
            Model.OccupiedCellModel.Entities.Add(Model.Id);
            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            UiController.UnhighlightActions();
        }
    }
}