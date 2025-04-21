using System;

public class TimerPresenter : IDisposable
{
    private Timer _timer;
    private TimerView _timerView;

    public TimerPresenter(Timer timer, TimerView timerView)
    {
        _timer = timer;
        _timerView = timerView;

        _timer.Changed += _timerView.ShowSeconds;
    }

    public void Dispose()
    {
        _timer.Changed -= _timerView.ShowSeconds;
    }
}