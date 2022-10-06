﻿using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class MainUiContext : MVCSContext
    {
        public MainUiContext(MainUIContextRoot view, bool autoMapping) : base(view, autoMapping)
        {
            injectionBinder.Bind<MainUIContextRoot>().ToSingleton().ToValue(view).CrossContext();
            var controller = new MainUiController(view);
            injectionBinder.Bind<MainUiController>().ToSingleton().ToValue(controller).CrossContext();
        }
    }
}