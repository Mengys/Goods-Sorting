using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _textTimer;
    [SerializeField] private int _startSeconds;  

    private Coroutine _timerCoroutine;
    private bool _isIncluded = false;

    public event Action Ended;

    private void OnEnable()
    {
        GameController.FirstMoveMade += StartTimer;
    }

    private void OnDisable()
    {
        GameController.FirstMoveMade -= StartTimer;
    }

    private void Start()
    {
        ShowSeconds();

        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        var delay = new WaitForSeconds(1f);

        while (enabled)
        {
            yield return delay;

            if (_isIncluded)
            {
                _startSeconds--;
                ShowSeconds();

                if(_startSeconds<= 0)
                {
                    StopTimer();
                    Ended?.Invoke();
                }
            }
        }
    }

    public void StartTimer() => _isIncluded = true;

    public void StopTimer() => _isIncluded = false;

    private void ShowSeconds() => _textTimer.text = _startSeconds.ToString();
}
