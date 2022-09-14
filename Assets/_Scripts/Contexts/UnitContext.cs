using _Scripts.Commands;
using strange.extensions.context.api;
using strange.extensions.context.impl;

namespace _Scripts
{
    public class UnitContext : MVCSContext
    {
        private readonly UnitContextRoot _view;
        private readonly int _id;

        public UnitContext(UnitContextRoot view, bool autoMapping, int id) : base(view, autoMapping)
        {
            _view = view;
            _id = id;
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<int>().ToValue(_id).ToName(UnitContextKeys.Id);
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);
            injectionBinder.injector.Inject(_view.AnimationEventHandler);

            commandBinder.Bind(GameEvents.MoveUnit).To<MoveToPositionCommand>();
            commandBinder.Bind(GameEvents.SelectUnit).To<ToggleSelectionCommand>();
            commandBinder.Bind(ContextEvent.START).To<SetupUnitCommand>().Once();
        }
    }
}