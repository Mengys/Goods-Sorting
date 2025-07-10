using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.GameState
{
    [CreateAssetMenu(fileName = "GameStateListConfig", menuName = "Configs/Lists/GameStateListConfig")]
    public class GameStateListConfig : ScriptableObject
    {
        [field: SerializeField] public List<GameStateConfig> States { get; private set; } = new()
        {
            new GameStateConfig { Id = GameStateId.Entry, SceneName = GameStateId.Entry.ToString() },
            new GameStateConfig { Id = GameStateId.Menu, SceneName = GameStateId.Menu.ToString() },
            new GameStateConfig { Id = GameStateId.Gameplay, SceneName = GameStateId.Gameplay.ToString() },
        };
    }
}