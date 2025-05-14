using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "ComboCollectAbility", menuName = "Configs/Abilities/ComboCollectAbility")]
    public class ComboCollectAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new ComboCollector();
    }
}