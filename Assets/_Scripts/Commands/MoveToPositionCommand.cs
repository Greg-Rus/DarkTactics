using System.Collections;
using System.Collections.Generic;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class MoveToPositionCommand : EventCommand
    {
        [Inject]  public UnitContextRoot RootView{get;set;}

        private const float KSpeed = 5f;

        public override void Execute()
        {
            Debug.Log("Starting coroutine");
            RootView.StartCoroutine(MoveToPosition());
        }

        private IEnumerator MoveToPosition()
        {
            var destination = (Vector3)evt.data;
            bool moving = true;
            while (moving)
            {
                var directionToDestination = destination - RootView.transform.position;
                var nextTranslationStep = directionToDestination.normalized * Time.deltaTime * KSpeed;
                if (directionToDestination.sqrMagnitude <= nextTranslationStep.sqrMagnitude)
                {
                    RootView.transform.position = destination;
                    moving = false;
                }
                else
                {
                    RootView.transform.Translate(nextTranslationStep);
                    yield return null;
                }
            }
        }
    }
}