using System.Collections.Generic;
using System.Linq;
using _Project.Code.Infrastructure.Services.GameStateMachine.State;
using UnityEngine;

namespace _Project.Code.Infrastructure.Services.GameStateMachine.Config
{
    [CreateAssetMenu(fileName = "GameStatesConfig", menuName = "Configs/GameStatesConfig")]
    public class GameStatesConfig : ScriptableObject
    {
        [SerializeField] private List<GameStateConfig> _stateScenes = new()
        {
            new GameStateConfig { stateId = GameStateId.Entry, SceneName = GameStateId.Entry.ToString() },
            new GameStateConfig { stateId = GameStateId.Menu, SceneName = GameStateId.Menu.ToString() },
            new GameStateConfig { stateId = GameStateId.Gameplay, SceneName = GameStateId.Gameplay.ToString() },
        };
        
        public Dictionary<GameStateId, string> StateScenes => 
            _stateScenes.ToDictionary(x => x.stateId, x => x.SceneName);
    }
}