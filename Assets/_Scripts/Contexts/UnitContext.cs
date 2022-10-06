using _Scripts.Commands;
using _Scripts.Commands.UnitCommands;
using _Scripts.Models;
using strange.extensions.context.impl;

namespace _Scripts
{
    public class UnitContext : MVCSContext
    {
        [Inject] public GridService GridService {get;set;}
        
        private readonly UnitContextRoot _view;
        private readonly int _id;
        private readonly UnitModel _model;

        public UnitContext(UnitContextRoot view, bool autoMapping, int id) : base(view, autoMapping)
        {
            _view = view;
            _id = id;
            _model = new UnitModel()
            {
                Id = _id,
                MovementRange = 5
            };
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<int>().ToValue(_id).ToName(UnitContextKeys.Id);
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);
            injectionBinder.Bind<UnitModel>().ToSingleton().ToValue(_model);
            injectionBinder.injector.Inject(_view.AnimationEventHandler);

            commandBinder.Bind(GameEvents.GridCellSelected).To<MoveToPositionCommand>();
            commandBinder.Bind(GameEvents.SelectUnit).To<ToggleUnitSelectionCommand>();
            commandBinder.Bind(GameEvents.SetupUnit).To<SetupUnitCommand>().Once();
            
            commandBinder.Bind(UnitEvents.MoveActionSelected).To<HandleMoveActionSelectedCommand>();
            commandBinder.Bind(UnitEvents.AttackActionSelected).To<HandleAttackSelectionCommand>();
        }
    }
}