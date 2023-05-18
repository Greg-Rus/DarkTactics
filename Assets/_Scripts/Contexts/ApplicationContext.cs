using strange.extensions.context.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
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
            injectionBinder.Bind<ActionSettingsConfig>().ToValue(_rootView.ActionSettingsConfig).ToSingleton().CrossContext();
            injectionBinder.Bind<ConstantsConfig>().ToValue(_rootView.ConstantsConfig).ToSingleton().CrossContext();
        }

        public override void Launch()
        {
            base.Launch();
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }
}