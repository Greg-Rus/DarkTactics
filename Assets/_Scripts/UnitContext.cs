using _Scripts.Commands;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class UnitContext : MVCSContext
    {
        private readonly UnitContextRoot _view;
        private readonly int _id;
        
        [Inject]
        public ApplicationLevelService test { private get; set; }

        public UnitContext(UnitContextRoot view, bool autoMapping, int id) : base(view, autoMapping)
        {
            _view = view;
            _id = id;
        }

        protected override void mapBindings()
        {
            injectionBinder.Bind<int>().ToValue(_id).ToName("Id");
            injectionBinder.Bind<UnitContextRoot>().ToSingleton().ToValue(_view);

            commandBinder.Bind(GameEvents.MoveUnit).To<MoveToPositionCommand>();
        }

        public override IContext Start()
        {
            Debug.Log($"test {test != null}");
            return base.Start();
        }
    }
}