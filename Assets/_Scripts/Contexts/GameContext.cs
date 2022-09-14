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
            injectionBinder.Bind<UnitRegistryService>().ToSingleton().CrossContext();
            injectionBinder.Bind<PrefabConfig>().ToSingleton()
                .ToValue(_view.PrefabConfig);
            injectionBinder.Bind<UnitModel>();
            injectionBinder.Bind<InputService>().ToSingleton().CrossContext();
            injectionBinder.Bind<GameSessionModel>().ToSingleton().CrossContext();
            injectionBinder.Bind<GridService>().ToSingleton()
                .ToValue(new GridService(20, 20, 1)).CrossContext();
            injectionBinder.Bind<VirtualCameraController>().ToSingleton()
                .ToValue(injectionBinder.injector.Inject(new VirtualCameraController(_view.VirtualCamera, _view.CameraTarget)));

            commandBinder.Bind(GameEvents.SpawnUnit).To<SpawnUnitCommand>();
            commandBinder.Bind(GameEvents.MouseClickGround)
                .To<MoveSelectedUnitCommand>()
                .To<SnapCameraToUnitDestination>()
                .InSequence();
            commandBinder.Bind(GameEvents.MouseClickUnit).To<ProcessUnitClickCommand>();
            commandBinder.Bind(GameEvents.SelectUnit).To<SelectUnitCommand>();
            commandBinder.Bind(GameEvents.ManualCameraMove).To<ManualMoveCameraCommand>().Pooled();
            
            commandBinder.Bind(ContextEvent.START)
                .To<InitializeServicesCommand>()
                .To<StartGameCommand>()
                .InSequence()
                .Once();
        }
    }
}