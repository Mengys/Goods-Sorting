using System;

public class Score
{
    private int _currentScore = 0;

    public event Action<int> Changed;

    public int CurrentScore => _currentScore;

    public void AddScore(int score)
    {
        _currentScore += score;
        Changed?.Invoke(_currentScore);
    }

    public void RemoveScore(int score)
    {
        _currentScore -= score;
        Changed?.Invoke(_currentScore);
    }
}
