using System.Collections;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class RotateToWorldPositionCommandAsync : ReturnCommand<IEnumerator>
    {
        private const float K_TurningSpeed = 5f;
        private readonly Vector3 targetPosition;
        [Inject] public UnitContextRoot RootView { private get; set; }
        public RotateToWorldPositionCommandAsync(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public override IEnumerator Execute()
        {
            var startRotation = RootView.transform.localRotation;
            var directionToDestination = targetPosition - RootView.transform.position;
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