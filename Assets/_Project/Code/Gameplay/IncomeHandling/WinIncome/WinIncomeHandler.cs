using _Project.Code.Gameplay.Counter;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.ProgressProvider;

namespace _Project.Code.Gameplay.IncomeHandling.WinIncome
{
    public class WinIncomeHandler : IWinIncomeHandler
    {
        private readonly IProgressProvider _progressProvider;
        private readonly IConfigProvider _configProvider;
        private readonly ICounter<Score> _scoreCounter;

        public WinIncomeHandler(
            IConfigProvider configProvider,
            IProgressProvider progressProvider,
            ICounter<Score> scoreCounter)
        {
            _configProvider = configProvider;
            _scoreCounter = scoreCounter;
            _progressProvider = progressProvider;
        }

        public int DefaultIncome =>
            _scoreCounter.Reactive.CurrentValue;

        public int RewardedIncome =>
            _scoreCounter.Reactive.CurrentValue * _configProvider.WinAdCoinsMultiplier;

        public void HandleDefault() => 
            _progressProvider.PlayerProgress.Coins += DefaultIncome;

        public void HandleRewarded() => 
            _progressProvider.PlayerProgress.Coins += RewardedIncome;
    }
}