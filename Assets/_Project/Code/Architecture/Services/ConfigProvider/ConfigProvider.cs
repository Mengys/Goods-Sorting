using System;
using System.Linq;
using _Project.Code.Architecture.Services.SoundPlayer;
using UnityEngine;

namespace _Project.Code
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/ConfigProvider")]
    public class ConfigProvider : ScriptableObject
    {
        [field: SerializeField] public GameStatesConfig GameStates { get; private set; }
        [field: SerializeField] public SoundsConfig Sounds { get; private set; }

        public SoundConfig GetConfigFor(SoundId id)
        {
            var config = Sounds.Sounds.FirstOrDefault(x => x.Id == id);

            if (config.Id == SoundId.None)
                throw new ArgumentException($"Sound with id {id} not found");
            
            return config;
        }
    }
}