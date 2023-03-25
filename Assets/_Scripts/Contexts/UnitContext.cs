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
        private EntityFasade _entityFasade;

        public EntityFasade EntityFasade => _entityFasade;

        public UnitContext(UnitContextRoot view, bool autoMapping, int id) : base(view, autoMapping)
        {
            _view = view;
            _id = id;
        }

        protected override void mapBindings()
        {
            var unitModel = new UnitModel()
            {
                Id = _id
            };
            var stateController = new UnitStateController();
            injectionBinder.injector.Inject(_view.AnimationEventHandler, false);
            
            injectionBinder.Bind<int>().ToValue(_id).ToName(UnitContextKeys.Id);
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);
            injectionBinder.Bind<Animator>().ToSingleton().ToValue(_view.Animator);
            injectionBinder.Bind<UnitModel>().ToSingleton().ToValue(unitModel);
            injectionBinder.Bind<EntityModel>().ToSingleton().ToValue(unitModel);
            injectionBinder.Bind<UnitStateController>().ToSingleton().ToValue(stateController);
            injectionBinder.Bind<AnimationEventHandler>().ToSingleton().ToValue(_view.AnimationEventHandler);
            injectionBinder.Bind<UnitSensor>().ToSingleton();

            commandBinder.Bind(UnitEvents.SetupUnit).To<SetupUnitCommand>().Once();
            commandBinder.Bind(UnitEvents.UnitSelected).To<ToggleUnitSelectionCommand>();
            commandBinder.Bind(UnitEvents.TurnStarted).To<HandleTurnStartCommand>();
            commandBinder.Bind(UnitEvents.HitTaken).To<PerformReceiveHitCommand>();
            //Action Selection
            commandBinder.Bind(InputEvents.MoveActionSelected)
                .To<HandleMoveActionSelectedCommand>()
                .To<VisualizeMoveActionSelectCommand>()
                .InSequence();
            commandBinder.Bind(InputEvents.AttackActionSelected)
                .To<HandleAttackSelectionCommand>()
                .To<VisualizeAttackSelectionCommand>()
                .InSequence();
            //Action Execution
            commandBinder.Bind(UnitEvents.GridCellSelected)
                .To<CanPerformMoveActionCommand>()
                .To<ConsumeResourcesForMoveActionCommand>()
                .To<BeginActionCommand>()
                .To<PerformMoveActionCommand>()
                .To<CompleteActionCommand>()
                .InSequence();
            commandBinder.Bind(UnitEvents.EnemySelected)
                .To<CanPerformAttackActionCommand>()
                .To<ConsumeResourcesForAttackActionCommand>()
                .To<BeginActionCommand>()
                .To<PerformAttackActionCommand>()
                .To<CompleteActionCommand>()
                .InSequence();
        }

        protected override void postBindings()
        {
            _entityFasade = injectionBinder.injector.Inject(new EntityFasade()) as EntityFasade;
            injectionBinder.Bind<EntityFasade>().ToSingleton().ToValue(_entityFasade);
        }
    }
}