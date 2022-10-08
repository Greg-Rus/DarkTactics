using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class MainUiContext : MVCSContext
    {
        public MainUiContext(MainUIContextRoot view, bool autoMapping) : base(view, autoMapping)
        {
            injectionBinder.Bind<MainUIContextRoot>().ToSingleton().ToValue(view).CrossContext();
            var controller = new UiController(view);
            controller.Initialize();
            injectionBinder.Bind<UiController>().ToSingleton().ToValue(controller).CrossContext();
        }
    }
}