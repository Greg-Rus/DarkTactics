using _Scripts.Commands;
using _Scripts.Models;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class UnitContext : MVCSContext
    {
        private readonly UnitContextRoot _view;
        private readonly int _id;
        private readonly UnitModel _model;

        public UnitContext(UnitContextRoot view, bool autoMapping, int id, Vector2Int initialPosition) : base(view, autoMapping)
        {
            _view = view;
            _id = id;
            _model = new UnitModel()
            {
                Id = _id,
                MovementRange = 5,
                OccupiedCellCoordinates = initialPosition
            };
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<int>().ToValue(_id).ToName(UnitContextKeys.Id);
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);
            injectionBinder.Bind<UnitModel>().ToSingleton().ToValue(_model);
            injectionBinder.injector.Inject(_view.AnimationEventHandler);

            commandBinder.Bind(GameEvents.MoveUnit).To<MoveToPositionCommand>();
            commandBinder.Bind(GameEvents.SelectUnit).To<ToggleSelectionCommand>();
            commandBinder.Bind(ContextEvent.START).To<SetupUnitCommand>().Once();
        }
    }
}