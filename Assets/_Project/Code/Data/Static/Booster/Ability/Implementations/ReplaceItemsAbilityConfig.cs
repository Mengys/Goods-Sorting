using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "ReplaceItemsAbility", menuName = "Configs/Abilities/ReplaceItemsAbility")]
    public class ReplaceItemsAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new ItemsReplacer();
    }
}