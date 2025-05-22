using _Project.Code.Data.Static.Booster.Ability;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster
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