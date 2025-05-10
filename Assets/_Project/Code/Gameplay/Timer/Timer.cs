using System;
using System.Collections;
using _Project.Code.Services.CoroutinePerformer;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.Timer
{
    public class Timer
    {
        private event Action ElapsedEvent;
        
        public Observable<Unit> Elapsed => Observable
            .FromEvent(
                a => ElapsedEvent += a,
                a => ElapsedEvent -= a);
        
        public event Action<float> Updated;
        
        private float _secondsLeft;
        private Coroutine _coroutine;
        private bool _isEnabled;

        private readonly ICoroutinePerformer _coroutinePerformer;

        public Timer(ICoroutinePerformer coroutinePerformer)
        {
            _coroutinePerformer = coroutinePerformer;
        }

        public float SecondsLeft => _secondsLeft;

        public void Initialize(float seconds) =>
            _secondsLeft = seconds;

        public void Start()
        {
            _isEnabled = true;

            _coroutine ??= _coroutinePerformer.Start(RunTimer());
        }

        public void Stop() => _isEnabled = false;

        private IEnumerator RunTimer()
        {
            float delayTime = 0.1f;
            
            var delay = new WaitForSeconds(delayTime);

            while (true)
            {
                yield return delay;

                if (!_isEnabled) continue;
                
                _secondsLeft -= delayTime;

                Updated?.Invoke(_secondsLeft);

                if (_secondsLeft <= 0)
                {
                    Stop();
                    ElapsedEvent?.Invoke();
                }
            }
        }
    }
}