using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Code
{
    [CreateAssetMenu(fileName = "GameStatesConfig", menuName = "Configs/GameStatesConfig")]
    public class GameStatesConfig : ScriptableObject
    {
        [SerializeField] private List<StateScene> _stateScenes = new()
        {
            new StateScene { State = GameState.Entry, SceneName = GameState.Entry.ToString() },
            new StateScene { State = GameState.Menu, SceneName = GameState.Menu.ToString() },
            new StateScene { State = GameState.Gameplay, SceneName = GameState.Gameplay.ToString() },
        };
        
        public Dictionary<GameState, string> StateScenes => 
            _stateScenes.ToDictionary(x => x.State, x => x.SceneName);
    }
}