using System.Collections.Generic;
using _Project.Code.Infrastructure.GameStateMachine.State;
using UnityEngine;

namespace _Project.Code.Infrastructure.GameStateMachine.Config
{
    [CreateAssetMenu(fileName = "GameStatesConfig", menuName = "Configs/GameStatesConfig")]
    public class GameStatesConfig : ScriptableObject
    {
        [field: SerializeField] public List<GameStateConfig> States { get; private set; } = new()
        {
            new GameStateConfig { Id = GameStateId.Entry, SceneName = GameStateId.Entry.ToString() },
            new GameStateConfig { Id = GameStateId.Menu, SceneName = GameStateId.Menu.ToString() },
            new GameStateConfig { Id = GameStateId.Gameplay, SceneName = GameStateId.Gameplay.ToString() },
        };
    }
}