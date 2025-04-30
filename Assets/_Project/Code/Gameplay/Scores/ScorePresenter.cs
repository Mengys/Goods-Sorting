using System;

namespace _Project.Code.Gameplay.Scores
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
            _scoreView.UpdateView(_score.CurrentScore);
        }

        public void Dispose()
        {
            _score.Changed -= _scoreView.UpdateView;
        }
    }
}
