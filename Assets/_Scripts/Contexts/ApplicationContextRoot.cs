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

        public ApplicationContext(ApplicationContextRoot view, bool autoStartup) : base(view, autoStartup)
        {
            _rootView = view;
        }
        
        protected override void mapBindings()
        {
            
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
}

