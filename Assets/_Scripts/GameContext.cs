using _Scripts.Commands;
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
            injectionBinder.Bind<GameContextRootMediator>().ToSingleton().ToValue(mediator);
            injectionBinder.Bind<UnitManager>().ToSingleton().CrossContext();
            injectionBinder.Bind<PrefabConfig>().ToSingleton().ToValue(_view.PrefabConfig);
            injectionBinder.Bind<UnitModel>();
            injectionBinder.Bind<InputService>().ToSingleton().CrossContext();

            commandBinder.Bind(GameEvents.SpawnUnit).To<SpawnUnitCommand>();
            commandBinder.Bind(ContextEvent.START)
                .To<InitializeServicesCommand>()
                .To<StartGameCommand>()
                .To<MoveSelectedUnitCommand>()
                .InSequence()
                .Once();
            
        }
    }
}