using System.Collections;
using _Scripts.Commands.UnitCommands;
using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class PerformMoveActionCommand : EventCommand<GridCellModel>
    {
        [Inject]  public UnitContextRoot RootView {get;set;}
        [Inject] public GridService GridService {get;set;}
        [Inject] public UnitModel Model {get;set;}

        private const float K_RunningSpeed = 5f;
        private const float K_TurningSpeed = 5f;

        public override void Execute()
        {
            Debug.Log($"{LogHelper.ActionTag} Moving...");
            Retain();
            RootView.StopAllCoroutines();
            var cellWorldPosition = GridService.GridCoordinateToWorldPosition(Payload.Coordinates);
            RootView.StartCoroutine(MoveToPosition(cellWorldPosition));
            RootView.StartCoroutine(RotateToPosition(cellWorldPosition));
        }
        
        private IEnumerator MoveToPosition(Vector3 destination)
        {
            OnDeparture();
            
            bool moving = true;
            RootView.Animator.SetBool(AnimationConstants.IsRunning, true);
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
        }

        private void OnArrival(Vector3 destination)
        {
            RootView.Animator.SetBool(AnimationConstants.IsRunning, false);
            Model.OccupiedCellModel = GridService.WorldPositionToGridCellModel(destination);
            Model.OccupiedCellModel.Entities.Add(Model.Id);
            Release();
        }
    }
}