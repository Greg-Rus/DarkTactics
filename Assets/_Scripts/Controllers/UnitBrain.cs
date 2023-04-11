using _Scripts.Helpers;
using _Scripts.Models;
using _Scripts.Services;
using strange.extensions.injector.api;

namespace _Scripts.Controllers
{
    public class UnitBrain
    {
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public UnitStateController UnitStateController { private get; set; }
        [Inject] public EnemyTurnService EnemyTurnService { private get; set; }
        [Inject] public AiBehaviourProvider AiBehaviourProvider { private get; set; }
        [Inject] public IInjectionBinder InjectionBinder { private get; set; }

        public void TakeTurn()
        {
            TakeNextAction();
        }

        public void TakeNextAction()
        {
            var bestAction = SelectBestAction();
            if (bestAction.ActionType == UnitActionType.None)
            {
                EnemyTurnService.UnitYieldsTurn();
            }
            else
            {
                AiBehaviourProvider.GetBehaviourExecutorCommand(bestAction).InjectWith(InjectionBinder).Execute();
            }
        }

        private AiAction SelectBestAction()
        {
            AiAction bestAction = new AiAction()
            {
                Score = 0,
                ActionType = UnitActionType.None
            };
            
            foreach (var behaviour in UnitModel.Settings.AiBehaviours)
            {
                var evaluatedAction = AiBehaviourProvider.GetBehaviourEvaluatorCommand(behaviour).InjectWith(InjectionBinder).Execute();
                
                if (!UnitStateController.CanPerformAction(evaluatedAction.ActionType))
                {
                    continue;
                }
                
                if (evaluatedAction.Score > bestAction.Score)
                {
                    bestAction = evaluatedAction;
                }
            }

            return bestAction;
        }
    }
}