using System.Collections;
using UnityEngine;

public class StopTimer : MonoBehaviour
{
    [SerializeField] private int _delay = 5;
    [SerializeField] private int _prise = 3;

    private Timer _timer;

    public int Prise => _prise;

    public void PauseForSeconds()
    {
        StartCoroutine(PauseTimer());
    }

    public void InitialuzeTimer(Timer timer)
    {
        _timer = timer;
    }

    private IEnumerator PauseTimer()
    {
        _timer.StopTimer();
        yield return new WaitForSeconds(_delay);
        _timer.StartTimer();
    }
}
