using _Scripts.Commands;
using _Scripts.Commands.UnitCommands;
using _Scripts.Controllers;
using _Scripts.Models;
using strange.extensions.context.impl;
using UnityEngine;

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
            var model = new UnitModel()
            {
                Id = _id
            };
            var stateController = new UnitStateController();
            injectionBinder.injector.Inject(_view.AnimationEventHandler, false);
            
            injectionBinder.Bind<int>().ToValue(_id).ToName(UnitContextKeys.Id);
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);
            injectionBinder.Bind<Animator>().ToSingleton().ToValue(_view.Animator);
            injectionBinder.Bind<UnitModel>().ToSingleton().ToValue(model);
            injectionBinder.Bind<UnitStateController>().ToSingleton().ToValue(stateController);
            injectionBinder.Bind<AnimationEventHandler>().ToSingleton().ToValue(_view.AnimationEventHandler);
            
            commandBinder.Bind(UnitEvents.SetupUnit).To<SetupUnitCommand>().Once();
            commandBinder.Bind(UnitEvents.GridCellSelected).To<PerformMoveActionCommand>();
            commandBinder.Bind(UnitEvents.UnitSelected).To<ToggleUnitSelectionCommand>();
            commandBinder.Bind(UnitEvents.EnemySelected).To<PerformAttackActionCommand>();
            commandBinder.Bind(UnitEvents.TurnStarted).To<HandleTurnStartCommand>();
            commandBinder.Bind(UnitEvents.HitTaken).To<PerformReceiveHitCommand>();
            
            
            commandBinder.Bind(InputEvents.MoveActionSelected).To<HandleMoveActionSelectedCommand>();
            commandBinder.Bind(InputEvents.AttackActionSelected).To<HandleAttackSelectionCommand>();
        }
    }
}