using strange.extensions.context.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    public class ApplicationContextRoot : ContextView
    {
        public GameContextRoot GameContextPrefab;

        [Header("Configs")]
        public PrefabConfig PrefabConfig;

        public UnitSettingsConfig UnitSettingsConfig;
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
            injectionBinder.Bind<PrefabConfig>().ToValue(_rootView.PrefabConfig).ToSingleton().CrossContext();
            injectionBinder.Bind<UnitSettingsConfig>().ToValue(_rootView.UnitSettingsConfig).ToSingleton().CrossContext();
        }

        public override void Launch()
        {
            base.Launch();
            Debug.Log("Application context is live");
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
            // var gameContextGo = GameObject.Instantiate(_rootView.GameContextPrefab);
            // var gameContext = new GameContext(gameContextGo, true);
            // gameContextGo.context = gameContext;
            // gameContext.Start();
        }
    }
}

