using System.Collections;
using UnityEngine;

namespace _Project.Code.Architecture
{
    public interface ICoroutinePerformer
    {
        Coroutine Start(IEnumerator coroutineFunction);
        void Stop(Coroutine coroutine);
    }
}