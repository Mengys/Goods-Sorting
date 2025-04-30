using System.Collections;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Timers;
using _Project.Code.Services.CoroutinePerformer;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class StopTimer : IAbility
    {
        private int _delay;
        private ICoroutinePerformer _coroutinePerformer;
        private Timer _timer;

        public StopTimer(int delay)
        {
            _delay = delay;
        }

        public void PauseForSeconds()
        {
            _coroutinePerformer.Start(PauseTimer());
        }

        public void Initialize(DiContainer container)
        {
            _timer = container.Resolve<Timer>();
            _coroutinePerformer = container.Resolve<ICoroutinePerformer>();
        }

        public void Use()
        {
            PauseForSeconds();
        }

        private IEnumerator PauseTimer()
        {
            _timer.StopTimer();
            yield return new WaitForSeconds(_delay);
            _timer.StartTimer();
        }
    }
}