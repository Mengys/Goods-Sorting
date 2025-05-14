using _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs
{
    [CreateAssetMenu(fileName = "BoosterConfig", menuName = "Configs/BoosterConfig")]
    public class BoosterAssetConfig : ScriptableObject
    {
        public AbilityConfig Ability;
        public Sprite Icon;
        public string Name;
        public int Price;
    }
}