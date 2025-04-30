using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "ShuffleAbility", menuName = "Configs/Abilities/ ShuffleAbility")]
    public class ShuffleAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility()
        {
            Shuffle shuffle = new Shuffle();

            return shuffle;
        }
    }
}