using System;
using System.Collections;
using _Project.Code.Services.CoroutinePerformer;
using _Project.Code.Services.PauseHandler;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Timer
{
    public class Timer : ITimer, IPausable, IDisposable
    {
        private Coroutine _coroutine;
        private bool _enabled;

        private readonly ReactiveProperty<float> _remainingSeconds = new(0);

        private readonly ICoroutinePerformer _coroutinePerformer;

        public Timer(ICoroutinePerformer coroutinePerformer)
        {
            _coroutinePerformer = coroutinePerformer;
        }

        public ReadOnlyReactiveProperty<float> RemainingSeconds =>
            _remainingSeconds;

        public Observable<Unit> Elapsed =>
            _remainingSeconds
                .Skip(1)
                .Where(_ => _remainingSeconds.Value <= 0)
                .AsUnitObservable();

        public bool IsRunning => _enabled; 

        public void Dispose()
        {
            if (_coroutine != null && _coroutinePerformer != null)
                _coroutinePerformer.Stop(_coroutine);
        }

        public void Pause() => Stop();

        public void Resume() => Start();

        public void Setup(float seconds) =>
            _remainingSeconds.Value = seconds;

        public void Start()
        {
            _enabled = true;
            _coroutine ??= _coroutinePerformer.Start(Routine());
        }

        public void Stop()
        {
            _enabled = false;
        }

        private IEnumerator Routine()
        {
            while (true)
            {
                yield return null;

                if (!_enabled) continue;

                _remainingSeconds.Value -= Time.deltaTime;

                if (_remainingSeconds.Value <= 0)
                    Stop();
            }
        }
    }
}