using _Project.Code.Architecture.Services.GameStateMachine;

namespace _Project.Code.Architecture.Configs
{
    [System.Serializable]
    public struct StateScene
    {
        public GameState State;
        public string SceneName;
    }
}