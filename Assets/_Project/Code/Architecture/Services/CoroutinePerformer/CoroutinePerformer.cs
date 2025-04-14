using System.Collections;
using UnityEngine;

namespace _Project.Code.Architecture
{
    public class CoroutinePerformer : ICoroutinePerformer
    {
        private readonly MonoBehaviour _monoBehaviour;
        
        public CoroutinePerformer(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }
        
        public Coroutine Start(IEnumerator coroutineFunction)
            => _monoBehaviour.StartCoroutine(coroutineFunction);

        public void Stop(Coroutine coroutine)
            => _monoBehaviour.StopCoroutine(coroutine);
    }
}