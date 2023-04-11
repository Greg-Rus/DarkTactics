using _Scripts.Commands;
using _Scripts.Controllers;
using _Scripts.Models;
using _Scripts.Services;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using Unity.VisualScripting;

namespace _Scripts
{
    public class GameContext : MVCSContext
    {
        private readonly GameContextRoot _view;
        public GameContext(GameContextRoot view, bool autoMapping) : base(view, autoMapping)
        {
            _view = view;
        }

        protected override void mapBindings()
        {
            var mediator = _view.AddComponent<GameContextRootMediator>();
            injectionBinder.Bind<GameContextRootMediator>().ToSingleton()
                .ToValue(mediator);
            injectionBinder.Bind<GameContextRoot>().ToSingleton()
                .ToValue(_view);
            injectionBinder.Bind<EntityRegistryService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GridService>().ToSingleton()
                .ToValue(new GridService(20, 20, 1)).CrossContext();
            injectionBinder.Bind<GridVisualsService>().ToSingleton()
                .ToValue(new GridVisualsService(_view.GridVisualsRoot, _view.GridVisualView)).CrossContext();
            injectionBinder.Bind<CoroutineService>().ToSingleton().CrossContext();
            injectionBinder.Bind<ProjectileFactory>().ToSingleton().CrossContext();
            //injectionBinder.Bind<UnitModel>();
            injectionBinder.Bind<InputService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GameSessionModel>().ToSingleton().CrossContext();
            injectionBinder.Bind<EnemyTurnService>().ToSingleton().CrossContext();;
            injectionBinder.Bind<VirtualCameraController>().ToSingleton()
                .ToValue(new VirtualCameraController(_view.VirtualCamera, _view.CameraTarget));

            commandBinder.Bind(GameEvent.SpawnUnit).To<SpawnUnitCommand>();
            commandBinder.Bind(GameEvent.MouseClickGround).To<MoveSelectedUnitCommand>();
            commandBinder.Bind(GameEvent.MouseClickUnit).To<ProcessUnitClickCommand>();
 
            commandBinder.Bind(GameEvent.MouseClickEnemy).To<ProcessEnemyClickCommand>();
            commandBinder.Bind(GameEvent.ManualCameraMove).To<ManualMoveCameraCommand>().Pooled();
            
            commandBinder.Bind(GameEvent.StartPlayerTurn).To<StartPlayerTurnCommand>();
            commandBinder.Bind(GameEvent.EndPlayerTurn).To<EndPlayerTurnCommand>();
            commandBinder.Bind(GameEvent.StartEnemyTurn).To<StartEnemyTurnCommand>();
            commandBinder.Bind(GameEvent.EndEnemyTurn).To<EndEnemyTurnCommand>();
            
            commandBinder.Bind(ContextEvent.START)
                .To<InitializeServicesCommand>()
                .To<LoadSubScenesCommand>()
                .To<StartGameCommand>()
                .InSequence()
                .Once();
        }
    }
}