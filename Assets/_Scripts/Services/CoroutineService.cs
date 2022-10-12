using System.Collections;
using UnityEngine;

namespace _Scripts
{
    public class CoroutineService
    {
        [Inject] public GameContextRoot GameContextRoot { private get; set; }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
             return GameContextRoot.StartCoroutine(coroutine);
        }
    }
}