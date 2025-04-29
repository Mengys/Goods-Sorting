using System.Collections.Generic;
using System.Linq;
using _Project.Code.Configs;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.UIFactory.Config;
using _Project.Code.UI.Window;

namespace _Project.Code.Services.ConfigProvider
{
    public class ConfigProvider : IConfigProvider
    {
        private Dictionary<GameStateId, GameStateConfig> _gameStateConfigs;
        private Dictionary<WindowId, WindowConfig> _windowConfigs;

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
        }

        public WindowConfig? ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out var config) ? config : null;

        public GameStateConfig? ForState(GameStateId stateId) =>
            _gameStateConfigs.TryGetValue(stateId, out var config) ? config : null;
    }
}