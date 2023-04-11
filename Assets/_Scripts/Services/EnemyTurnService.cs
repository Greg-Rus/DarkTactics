using System.Collections.Generic;
using System.Linq;
using _Scripts.Commands;
using _Scripts.Helpers;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.injector.api;

namespace _Scripts.Services
{
    public class EnemyTurnService
    {
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        [Inject(ContextKeys.CONTEXT_DISPATCHER)] public IEventDispatcher EventDispatcher { private get; set; }

        private Queue<int> _turnOrderQueue;


        public EnemyTurnService()
        {
            _turnOrderQueue = new Queue<int>();
        }

        public void ExecuteEnemyTurn()
        {
            _turnOrderQueue.Clear();

            foreach (int enemyId in EntityRegistryService.GetAllEnemyIds())
            {
                _turnOrderQueue.Enqueue(enemyId);
            }

            RunUnitActionsOrEndTurn();
        }

        public void UnitYieldsTurn()
        {
            RunUnitActionsOrEndTurn();
        }

        private void RunUnitActionsOrEndTurn()
        {
            if (_turnOrderQueue.Any())
            {
                DispatchTakeTurnEvent(_turnOrderQueue.Dequeue());
            }
            else
            {
                EventDispatcher.Dispatch(GameEvent.EndEnemyTurn);
            }
        }

        private void DispatchTakeTurnEvent(int unitId)
        {
            var unitFasade = EntityRegistryService.GetFasadeById(unitId);
            unitFasade.EventDispatcher.Dispatch(UnitEvents.TakeTurn);
        }
    }
}