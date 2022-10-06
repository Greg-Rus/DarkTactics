using System.Linq;
using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace _Scripts.Commands
{
    public class StartGameCommand : EventCommand
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)]
        public IEventDispatcher Dispatcher { get; set; }

        [Inject] public GridService GridService { get; set; }
        [Inject] public MainUiController MainUiController { private get; set; }
        [Inject] public UnitSettingsConfig UnitSettingsConfig { private get; set; }

        public override void Execute()
        {
            GridService.Initialize();
            Dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload()
                {
                    Id = 1,
                    InitialPosition = new Vector2Int(1, 0),
                    Settings = UnitSettingsConfig.UnitSettings.First(config => config.Type == UnityTypes.TestUnit)
                }
            );
            Dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload()
            {
                Id = 2,
                InitialPosition = new Vector2Int(4, 0),
                Settings = UnitSettingsConfig.UnitSettings.First(config => config.Type == UnityTypes.TestNoAttackUnit)
            });
        }
    }
}