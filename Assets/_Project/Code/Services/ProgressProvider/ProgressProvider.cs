using System;
using _Project.Code.Data.Dynamic;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Data.Static.Level;
using _Project.Code.Services.ConfigProvider;

namespace _Project.Code.Services.ProgressProvider
{
    public class ProgressProvider : IProgressProvider, IDisposable
    {
        private IConfigProvider _configProvider;

        public ProgressProvider(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }
        
        public ReactivePlayerProgress PlayerProgress { get; } = new();

        public void IncrementPassedLevel()
        {
            var nextLevelId = PlayerProgress.Level.Id + 1;

            LevelConfig? config = _configProvider.ForLevel(nextLevelId);

            if (config != null)
            {
                PlayerProgress.Level = new LevelInfo
                {
                    Id = nextLevelId,
                    Difficulty = config.Value.Difficulty
                };
            }
        }
        
        public void Dispose() => 
            PlayerProgress?.Dispose();
    }
}