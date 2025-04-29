using System.Collections;
using UnityEngine;

namespace _Project.Code.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        Coroutine Start(IEnumerator routine);
        void Stop(Coroutine routine);
    }
}