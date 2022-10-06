using System.Collections;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace _Scripts.Commands
{
    public class LoadSubScenesCommand : EventCommand
    {
        [Inject] public GameContextRoot GameContextRoot { private get; set; }
        public override void Execute()
        {
            Retain();
            GameContextRoot.StartCoroutine(LoadSubScenesAsync());
        }
        
        private IEnumerator LoadSubScenesAsync()
        {
            yield return SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            Release();
        }
    }
}