using System;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.UI.Buttons.Window;

namespace _Project.Code.Gameplay.IncomeHandling.ScoreIncome
{
    public class ScoreIncomeHandler : IScoreIncomeHandler
    {
        private readonly ICounter<Score> _counter;
        private readonly IComboHandler _comboHandler;
        private readonly IConfigProvider _configProvider;

        public ScoreIncomeHandler(
            ICounter<Score> counter,
            IConfigProvider configProvider,
            IComboHandler comboHandler = null)
        {
            _configProvider = configProvider;
            _comboHandler = comboHandler;
            _counter = counter;
        }

        public void Handle(int itemsCount)
        {
            var config = _configProvider.ScoreIncomeConfig;

            if (!config.HasValue)
                throw new NullReferenceException(nameof(config));

            int income = config.Value.MatchIncome;

            income *= itemsCount;
            income *= _comboHandler?.Level.CurrentValue ?? 1;

            income = Math.Max(3, income);
            
            _counter.Add(income);
        }
    }
}