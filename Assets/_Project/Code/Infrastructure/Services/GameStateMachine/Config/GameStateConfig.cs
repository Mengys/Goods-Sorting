using _Project.Code.Infrastructure.Services.GameStateMachine.State;
using UnityEngine.Serialization;

namespace _Project.Code.Infrastructure.Services.GameStateMachine.Config
{
    [System.Serializable]
    public struct GameStateConfig
    {
        [FormerlySerializedAs("State")] public GameStateId stateId;
        public string SceneName;
    }
}