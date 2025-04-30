using System.Collections.Generic;
using System.Linq;
using _Project.Code.Configs;
using _Project.Code.Gameplay.Boosters.Configs.AbilityConfigProviders;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.ParticlesPlayer.Config;
using _Project.Code.Services.SoundPlayer.Config;
using _Project.Code.Services.UIFactory.Config;
using _Project.Code.UI.Window;

namespace _Project.Code.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        private Dictionary<GameStateId, GameStateConfig> _gameStateConfigs;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<ParticleId, ParticleConfig> _particleConfigs;
        private Dictionary<SoundId, SoundConfig> _soundConfigs;

        public ConfigProvider(GameConfig config)
        {
            _gameStateConfigs = config
                .GameStatesConfig
                .States
                .ToDictionary(x => x.Id, x => x);

            _windowConfigs = config
                .WindowsConfig
                .Windows
                .ToDictionary(x => x.Id, x => x);

            _particleConfigs = config
                .ParticlesConfig
                .ParticleConfigs
                .ToDictionary(x => x.Id, x => x);
            
            _soundConfigs = config
                .SoundsConfig
                .Sounds
                .ToDictionary(x => x.Id, x => x);
        }

        public WindowConfig? ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public GameStateConfig? ForState(GameStateId id) =>
            _gameStateConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public ParticleConfig? ForParticle(ParticleId id) =>
            _particleConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public SoundConfig? ForSound(SoundId id) => 
            _soundConfigs.TryGetValue(id, out var config) ? config : throw new KeyNotFoundException(id.ToString());

        public AbilityConfigProvider AbilityConfigProvider { get; set; }
    }
}