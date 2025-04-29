using _Project.Code.Architecture.Configs;
using UnityEngine;

namespace _Project.Code.Architecture.Services.ConfigProvider
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/ConfigProvider")]
    public class ConfigProvider : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStates { get; private set; }
    }
}