using System.Collections;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class StartEnemyTurnCommand : EventCommand
    {
        [Inject] public GameSessionModel GameSessionModel { private get; set; }
        [Inject] public UiController UiController { private get; set; }
        [Inject] public CoroutineService CoroutineService { private get; set; }
        public override void Execute()
        {
            GameSessionModel.PlayersTurn = false;
            CoroutineService.StartCoroutine(MockTurn());
        }

        private IEnumerator MockTurn()
        {
            yield return new WaitForSeconds(3);
            dispatcher.Dispatch(GameEvents.EndEnemyTurn);
        }
    }
}