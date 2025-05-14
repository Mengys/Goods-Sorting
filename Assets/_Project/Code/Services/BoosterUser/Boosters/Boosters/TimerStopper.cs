using System.Collections;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Timer;
using _Project.Code.Services.CoroutinePerformer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class TimerStopper : IAbility
    {
        private ICoroutinePerformer _coroutinePerformer;
        private ITimer _timer;
        
        private readonly float _duration;

        public TimerStopper(float duration = 10f)
        {
            _duration = duration;
        }

        public void Initialize(DiContainer container)
        {
            _timer = container.Resolve<ITimer>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
        }

        public void Use() => 
            _coroutinePerformer.Start(Pause());

        private IEnumerator Pause()
        {
            _timer.Stop();
            yield return new WaitForSeconds(_duration);
            _timer.Start();
        }
    }
}