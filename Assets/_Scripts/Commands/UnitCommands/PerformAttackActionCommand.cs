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
        [Inject]  public UnitContextRoot RootView {get;set;}

        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public Animator Animator { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }

        private bool shootEventReceived = false;
        private bool attackEndEventReceived = false;
        private bool isAttacking = false;
        

        public override void Execute()
        {
            if(UnitModel.SelectedAction != UnitActionTypes.Attack || 
               isAttacking ||
               UnitStateController.TryDeductActionPointsForAction(UnitActionTypes.Attack) == false) return;
            
            new UpdateUnitUiCommand().InjectWith(injectionBinder).Execute();
            RootView.StartCoroutine(DoAttack());
        }

        private IEnumerator DoAttack()
        {
            isAttacking = true;
            yield return new RotateToWorldPositionCommandAsync(Payload.Target.position).InjectWith(injectionBinder).Execute();
            yield return AnimateAttackAndWait();
            yield return SpawnAttackAndWait();
            UnitModel.SelectedAction = UnitActionTypes.None;
            isAttacking = false;
            new HandleAttackCompleteCommand().InjectWith(injectionBinder).Execute();
        }


        private IEnumerator AnimateAttackAndWait()
        {
            RootView.Animator.SetTrigger(AnimatorParameters.CastSpell);
            dispatcher.AddListener(UnitEvents.SpellCastShoot, OnShootEvent);
            yield return new WaitUntil(() => shootEventReceived);
        }
        
        private void OnShootEvent()
        {
            dispatcher.RemoveListener(UnitEvents.SpellCastShoot, OnShootEvent);
            shootEventReceived = true;
        }
        
        private IEnumerable SpawnAttackAndWait()
        {
            Debug.Log("Would emit spell");
            dispatcher.AddListener(UnitEvents.SpellCastFinished, OnAttackFinishedEvent);
            yield return new WaitUntil(() => attackEndEventReceived);
        }
        
        private void OnAttackFinishedEvent()
        {
            dispatcher.RemoveListener(UnitEvents.SpellCastFinished, OnAttackFinishedEvent);
            attackEndEventReceived = true;
        }
    }
}