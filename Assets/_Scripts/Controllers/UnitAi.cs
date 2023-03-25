using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Models;
using strange.extensions.dispatcher.eventdispatcher.api;

namespace _Scripts.Controllers
{
    public class UnitAi
    {
        [Inject] public UnitSensor UnitSensor { private get; set; }
        [Inject] public UnitModel UnitModel { private get; set; }
        [Inject] public IEventDispatcher EventDispatcher { private get; set; }
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        [Inject] public UnitStateController UnitStateController { private get; set; }

        public void TakeTurn()
        {
            
        }

        private void SelectBestAction()
        {
            var bestAction = UnitActionTypes.None;
            foreach (var action in UnitModel.Settings.Actions)
            {
                if (UnitStateController.CanPerformAction(action))
                {
                    EvaluateAction(action);
                }
            }
        }

        private void EvaluateAction(UnitActionTypes actionType)
        {
            EventDispatcher.Dispatch(actionType);
        }

        private void EvaluateBestAttack()
        {
            var playerUnitIDs = EntityRegistryService.GetAllPlayerUnitId();
        }

        public struct AiAction
        {
            public UnitActionTypes ActionType;
            public GridCellModel GridCellTarget;
            public int UnitId;
        }
    }
}