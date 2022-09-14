using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveToPositionCommand : EventCommand
    {
        [Inject]  public UnitContextRoot RootView{get;set;}

        private const float K_RunningSpeed = 5f;
        private const float K_TurningSpeed = 5f;

        public override void Execute()
        {
            Debug.Log("Starting coroutine");
            RootView.StopAllCoroutines();
            var destination = (Vector3)evt.data;
            
            RootView.StartCoroutine(MoveToPosition(destination));
            RootView.StartCoroutine(RotateToPosition(destination));
        }

        private IEnumerator MoveToPosition(Vector3 destination)
        {
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
            RootView.Animator.SetBool(AnimatorParameters.IsRunning, false);
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
    }
}