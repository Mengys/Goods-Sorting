using System.Collections;
using UnityEngine;

namespace _Project.Code.Architecture.Services.CoroutinePerformer
{
    public interface ICoroutinePerformer
    {
        Coroutine Start(IEnumerator routine);
        void Stop(Coroutine routine);
    }
}