using ParticlesPlayer;
using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Services.UIFactory.Config;
using UnityEngine;

namespace _Project.Code.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStatesConfig { get; private set; }
        [field: SerializeField] public WindowsConfig WindowsConfig { get; private set; }
        [field: SerializeField] public ParticlesConfig ParticlesConfig { get; private set; }
    }
}