using System.Linq;
using _Scripts.EventPayloads;
using _Scripts.Models;
using strange.extensions.command.impl;
using UnityEngine;

namespace _Scripts.Commands
{
    public class StartGameCommand : EventCommand
    {
        [Inject] public GridService GridService { get; set; }
        [Inject] public UnitSettingsConfig UnitSettingsConfig { private get; set; }
        [Inject] public EntityRegistryService EntityRegistryService { private get; set; }

        public override void Execute()
        {
            GridService.Initialize();
            dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload()
                {
                    Id = EntityRegistryService.NextEntityId,
                    InitialPosition = new Vector2Int(1, 0),
                    Settings = UnitSettingsConfig.UnitSettings.First(config => config.UnitType == UnitTypes.TestUnit)
                }
            );
            
            dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload()
            {
                Id = EntityRegistryService.NextEntityId,
                InitialPosition = new Vector2Int(4, 0),
                Settings = UnitSettingsConfig.UnitSettings.First(config => config.UnitType == UnitTypes.TestLongRangeUnit)
            });
            
            dispatcher.Dispatch(GameEvents.SpawnUnit, new SpawnEventPayload()
            {
                Id = EntityRegistryService.NextEntityId,
                InitialPosition = new Vector2Int(2, 4),
                Settings = UnitSettingsConfig.UnitSettings.First(config => config.UnitType == UnitTypes.TestEnemy)
            });
            
            dispatcher.Dispatch(GameEvents.StartPlayerTurn);
        }
    }
}