using System.Collections;
using _Scripts.Controllers;
using _Scripts.EventPayloads;
using _Scripts.Helpers;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class PerformAttackActionCommand : EventCommand<AttackActionPayload>
    {
        [Inject] public UnitContextRoot RootView { get; set; }

        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public Animator Animator { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public ProjectileFactory ProjectileFactory { private get; set; }

        private bool shootEventReceived = false;
        private bool attackEndEventReceived = false;
        private bool isAttacking = false;


        public override void Execute()
        {
            if (UnitModel.SelectedAction != UnitActionTypes.Attack ||
                isAttacking ||
                UnitStateController.TryDeductActionPointsForAction(UnitActionTypes.Attack) == false ||
                IsCellInRange(Payload.TargetCoordinates) == false)
            {
                return;
            }

            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            new PrepareForUnitActionCommand().InjectWith(injectionBinder).Execute();

            RootView.StartCoroutine(DoAttack(Payload.TargetTransform));
        }

        private IEnumerator DoAttack(Transform target)
        {
            isAttacking = true;

            yield return new RotateToWorldPositionCommandAsync(Payload.TargetTransform.position)
                .InjectWith(injectionBinder).Execute();
            yield return AnimateAttackAndWait();
            yield return SpawnAttackAndWait(target);
            UnitModel.SelectedAction = UnitActionTypes.None;
            isAttacking = false;
            new HandleAttackCompleteCommand().InjectWith(injectionBinder).Execute();
        }

        private IEnumerator AnimateAttackAndWait()
        {
            RootView.Animator.SetTrigger(AnimationConstants.Attack);
            dispatcher.AddListener(AnimationEvents.AttackEmit, OnShootEvent);
            yield return new WaitUntil(() => shootEventReceived);
        }

        private void OnShootEvent()
        {
            dispatcher.RemoveListener(AnimationEvents.AttackEmit, OnShootEvent);
            shootEventReceived = true;
        }

        private IEnumerator SpawnAttackAndWait(Transform target)
        {
            var projectile = ProjectileFactory.SpawnProjectile();
            projectile.Init(UnitModel.Settings.AttackDamageEffect);
            projectile.transform.position = RootView.RightHandSpawnPoint.position;
            var targetPosition = target.position + Vector3.up * RootView.RightHandSpawnPoint.position.y;
            projectile.transform.LookAt(targetPosition);
            projectile.Rigidbody.AddForce(projectile.transform.forward * 400f);

            dispatcher.AddListener(AnimationEvents.AttackFinished, OnAttackFinishedEvent);
            yield return new WaitUntil(() => attackEndEventReceived);
            new CleanUpAfterUnitActionCommand().InjectWith(injectionBinder).Execute();
        }

        private void OnAttackFinishedEvent()
        {
            dispatcher.RemoveListener(AnimationEvents.AttackFinished, OnAttackFinishedEvent);
            attackEndEventReceived = true;
        }

        private bool IsCellInRange(Vector2 coordinates)
        {
            var isInRange = false;
            foreach (var cellModel in UnitModel.ActionRangeCells)
            {
                if (cellModel == null) continue;
                if (cellModel.Coordinates == coordinates)
                {
                    isInRange = true;
                    break;
                }
            }

            return isInRange;
        }
    }
}