using System.Collections;
using _Scripts.Models;
using _Scripts.Services;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class StartEnemyTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public EnemyTurnService EnemyTurnService { private get; set; }

        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }
        public override void Execute()
        {
            GameSessionModel.PlayersTurn = false;

            foreach (var id in EntityRegistryService.GetAllEnemyIds())
            {
                EntityRegistryService.GetFasadeById(id)
                    .EventDispatcher
                    .Dispatch(UnitEvents.TurnStarted);
            }
            
            EnemyTurnService.ExecuteEnemyTurn();
        }
    }
}