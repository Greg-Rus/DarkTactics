using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class ApplicationContextRoot : ContextView
    {
        public GameContextRoot GameContextPrefab;
        // Start is called before the first frame update
        void Awake()
        {
            context = new ApplicationContext(this, true);
            context.Start ();
        }
    }

    public class ApplicationContext : MVCSContext
    {
        private ApplicationContextRoot _rootView;
        public ApplicationContext()
        {
        }

        public ApplicationContext(ApplicationContextRoot view, bool autoStartup) : base(view, autoStartup)
        {
            _rootView = view;
        }

        // protected override void addCoreComponents()
        // {
        //     base.addCoreComponents();
        //     injectionBinder.Unbind<ICommandBinder>();
        //     injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
        // }
        
        protected override void mapBindings()
        {
            injectionBinder.CrossContextBinder.Bind<ApplicationLevelService>().ToSingleton();
        }

        public override void Launch()
        {
            base.Launch();
            Debug.Log("Application context is live");
            
            var gameContextGo = GameObject.Instantiate(_rootView.GameContextPrefab);
            var gameContext = new GameContext(gameContextGo, true);
            gameContextGo.context = gameContext;
            gameContext.Start();
        }
    }

    public class ApplicationLevelService
    {
        public void Use()
        {
            Debug.Log("Service in use");
        }
    }
}

