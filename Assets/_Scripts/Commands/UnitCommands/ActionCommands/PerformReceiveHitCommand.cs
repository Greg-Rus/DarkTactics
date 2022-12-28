using _Scripts.Models;
using strange.extensions.command.impl;
using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Commands.UnitCommands
{
    public class PerformReceiveHitCommand : EventCommand<DamageEffectConfig>
    {
        [Inject] public Animator Animator { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        public override void Execute()
        {
            UnitModel.State.HitPoints = math.max(0, UnitModel.State.HitPoints - Payload.Damage);
            if (UnitModel.State.HitPoints == 0)
            {
                Animator.SetTrigger(AnimationConstants.Die);
            }
            else
            {
                Animator.SetTrigger(AnimationConstants.TakeHit);
            }
            Debug.Log($"Unit ID: {UnitModel.Id} took {Payload.Damage} damage. Current HP: {UnitModel.State.HitPoints}");
        }
    }
}