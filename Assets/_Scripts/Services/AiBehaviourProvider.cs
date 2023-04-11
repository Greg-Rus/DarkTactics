using System;
using _Scripts.Commands.UnitCommands;
using _Scripts.Commands.UnitCommands.AiCommands;
using _Scripts.Controllers;
using _Scripts.Models;
using strange.extensions.command.impl;

namespace _Scripts.Services
{
    public class AiBehaviourProvider
    {
        public ReturnCommand<AiAction> GetBehaviourEvaluatorCommand(AiBehaviour aiBehaviour)
        {
            switch (aiBehaviour.Behaviour)
            {
                case AiBehaviourType.MoveToClosestUnit:
                    return new EvaluateMoveToMeleeRangeOfClosestPlayerUnitCommand(aiBehaviour);
                case AiBehaviourType.MeleeAttack:
                    return new EvaluateAttackInMeleeRangeCommand(aiBehaviour);
                case AiBehaviourType.MoveToProjectileRange:
                    return new EvaluateMoveToFiringRangeOfClosestPlayerUnitCommand(aiBehaviour);
                case AiBehaviourType.ProjectileAttack:
                    return new EvaluateRangedAttackClosestCommand(aiBehaviour);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(aiBehaviour), aiBehaviour, $"No evaluators defined for {aiBehaviour}");
            }
        }
        
        public Command GetBehaviourExecutorCommand(AiAction action)
        {
            switch (action.ActionType)
            {
                case UnitActionType.Move:
                    return new AiExecuteMoveToDestinationCommand(action);
                case UnitActionType.Attack:
                    return new AiExecuteAttackCommand(action);
                    
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}