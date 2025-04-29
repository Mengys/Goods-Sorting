using _Project.Code.Infrastructure.Services.GameStateMachine.Config;
using UnityEngine;

namespace _Project.Code.Infrastructure.Services.ConfigProvider
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/ConfigProvider")]
    public class ConfigProvider : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStates { get; private set; }
    }
}