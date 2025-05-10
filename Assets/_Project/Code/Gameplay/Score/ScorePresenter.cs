using System;
using UnityEngine;

namespace _Project.Code.Gameplay.Score
{
    public class ScorePresenter : IDisposable
    {
        private Score _score;
        private ScoreView _scoreView;

        public ScorePresenter(Score score, ScoreView scoreView)
        {
            _score = score;
            _scoreView = scoreView;

            _score.Changed += _scoreView.UpdateView;
            _scoreView.UpdateView(_score.Value);
        }
        
        public void AddScore(int score) => 
            _score.AddScore(score);

        public void Dispose()
        {
            _score.Changed -= _scoreView.UpdateView;
        }
    }
}
