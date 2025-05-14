using System.Collections;
using UnityEngine;

namespace _Project.Code.Services.CoroutinePerformer
{
    public class CoroutinePerformer : ICoroutinePerformer
    {
        private readonly MonoBehaviour _monoBehaviour;
        
        public CoroutinePerformer(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }
        
        public Coroutine Start(IEnumerator routine)
            => _monoBehaviour?.StartCoroutine(routine);

        public void Stop(Coroutine routine)
            => _monoBehaviour?.StopCoroutine(routine);
    }
}