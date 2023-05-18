using strange.extensions.context.impl;
using UnityEngine;

namespace _Scripts
{
    public class ApplicationContextRoot : ContextView
    {
        public GameContextRoot GameContextPrefab;

        [Header("Configs")]
        public PrefabConfig PrefabConfig;
        public UnitSettingsConfig UnitSettingsConfig;
        public ActionSettingsConfig ActionSettingsConfig;
        public ConstantsConfig ConstantsConfig;
        // Start is called before the first frame update
        void Awake()
        {
            context = new ApplicationContext(this, true);
            context.Start ();
        }
    }
}