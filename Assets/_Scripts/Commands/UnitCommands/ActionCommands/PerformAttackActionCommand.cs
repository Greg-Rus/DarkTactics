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
        [Inject] public UnitContextRoot RootView { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public ProjectileFactory ProjectileFactory { private get; set; }

        [Inject] public ConstantsConfig ConstantsConfig { private get; set; }

        private bool shootEventReceived = false;
        private bool attackEndEventReceived = false;

        public override void Execute()
        {
            Debug.Log($"{LogHelper.ActionTag} Attacking...");
            RootView.StartCoroutine(DoAttack(Payload.TargetTransform));
        }

        private IEnumerator DoAttack(Transform target)
        {
            Retain();
            UnitModel.IsAttacking = true;

            yield return new RotateToWorldPositionCommandAsync(Payload.TargetTransform.position)
                .InjectWith(injectionBinder).Execute();
            yield return AnimateAttackAndWait();
            yield return SpawnAttackAndWait(target);
            
            UnitModel.IsAttacking = false;
            Release();
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
            var projectileLayer = UnitModel.EntityType == EntityType.PlayerUnit
                ? ConstantsConfig.PlayerProjectileLayer
                : ConstantsConfig.EnemyProjectileLayer;
            projectile.Init(UnitModel.Settings.AttackDamageEffect, projectileLayer);
            projectile.transform.position = RootView.RightHandSpawnPoint.position;
            var targetPosition = target.position + Vector3.up * RootView.RightHandSpawnPoint.position.y;
            projectile.transform.LookAt(targetPosition);
            projectile.Rigidbody.AddForce(projectile.transform.forward * 400f);

            dispatcher.AddListener(AnimationEvents.AttackFinished, OnAttackFinishedEvent);
            yield return new WaitUntil(() => attackEndEventReceived);
        }

        private void OnAttackFinishedEvent()
        {
            dispatcher.RemoveListener(AnimationEvents.AttackFinished, OnAttackFinishedEvent);
            attackEndEventReceived = true;
        }

        private bool IsTileInRange(Vector2 coordinates)
        {
            var isInRange = false;
            foreach (var tileModel in UnitModel.ActionRangeTiles)
            {
                if (tileModel == null) continue;
                if (tileModel.Coordinates == coordinates)
                {
                    isInRange = true;
                    break;
                }
            }

            return isInRange;
        }
    }
}