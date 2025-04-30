using System;
using System.Collections;
using _Project.Code.Services.CoroutinePerformer;
using UnityEngine;

public class Timer
{
    private float _seconds;
    private Coroutine _coroutine;
    private bool _isEnabled = false;
    private ICoroutinePerformer _coroutinePerformer;

    public float Seconds => _seconds;
    public event Action Ended;

    public event Action<float> Changed;

    public Timer(ICoroutinePerformer coroutinePerformer, float seconds)
    {
        _coroutinePerformer = coroutinePerformer;
        _seconds = seconds;
    }

    public void StartTimer()
    {
        _isEnabled = true;

        if (_coroutine == null)
        {
            _coroutine = _coroutinePerformer.Start(RunTimer());
        }
    }

    public void StopTimer() => _isEnabled = false;

    private IEnumerator RunTimer()
    {
        float delayTime = 0.1f;
        var delay = new WaitForSeconds(delayTime);

        while (true)
        {
            yield return delay;

            if (_isEnabled)
            {
                _seconds -= delayTime;

                Changed?.Invoke(_seconds);

                if (_seconds <= 0)
                {
                    StopTimer();
                    Ended?.Invoke();
                }
            }
        }
    }
}