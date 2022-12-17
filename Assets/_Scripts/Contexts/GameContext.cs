using _Scripts.Commands;
using _Scripts.Controllers;
using _Scripts.Models;
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
            injectionBinder.Bind<CoroutineService>().ToSingleton();
            injectionBinder.Bind<ProjectileFactory>().ToSingleton().CrossContext();
            injectionBinder.Bind<UnitModel>();
            injectionBinder.Bind<InputService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GameSessionModel>().ToSingleton().CrossContext();
            injectionBinder.Bind<VirtualCameraController>().ToSingleton()
                .ToValue(new VirtualCameraController(_view.VirtualCamera, _view.CameraTarget));

            commandBinder.Bind(GameEvents.SpawnUnit).To<SpawnUnitCommand>();
            commandBinder.Bind(GameEvents.MouseClickGround)
                .To<MoveSelectedUnitCommand>()
                //.To<SnapCameraToUnitDestination>() TODO: This was just a test. Ideally the camera should follow the unit so this should be requested by the unit itself.
                .InSequence();
            commandBinder.Bind(GameEvents.MouseClickUnit).To<ProcessUnitClickCommand>();
 
            commandBinder.Bind(GameEvents.MouseClickEnemy).To<ProcessEnemyClickCommand>();
            commandBinder.Bind(GameEvents.ManualCameraMove).To<ManualMoveCameraCommand>().Pooled();
            
            commandBinder.Bind(GameEvents.StartPlayerTurn).To<StartPlayerTurnCommand>();
            commandBinder.Bind(GameEvents.EndPlayerTurn).To<EndPlayerTurnCommand>();
            commandBinder.Bind(GameEvents.StartEnemyTurn).To<StartEnemyTurnCommand>();
            commandBinder.Bind(GameEvents.EndEnemyTurn).To<EndEnemyTurnCommand>();
            
            commandBinder.Bind(ContextEvent.START)
                .To<InitializeServicesCommand>()
                .To<LoadSubScenesCommand>()
                .To<StartGameCommand>()
                .InSequence()
                .Once();
        }
    }
}