using UnityEngine.Serialization;

namespace _Project.Code.Data.Static.GameState
{
    [System.Serializable]
    public struct GameStateConfig
    {
        [FormerlySerializedAs("StateId")] public GameStateId Id;
        public string SceneName;
    }
}