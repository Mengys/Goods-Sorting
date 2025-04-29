using _Project.Code.Infrastructure.GameStateMachine.State;
using UnityEngine.Serialization;

namespace _Project.Code.Infrastructure.GameStateMachine.Config
{
    [System.Serializable]
    public struct GameStateConfig
    {
        [FormerlySerializedAs("StateId")] public GameStateId Id;
        public string SceneName;
    }
}