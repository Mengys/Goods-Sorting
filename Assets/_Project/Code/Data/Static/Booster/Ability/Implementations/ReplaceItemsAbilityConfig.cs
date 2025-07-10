using _Project.Code.Services.BoosterUser.Boosters.Ability;
using _Project.Code.Services.BoosterUser.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability.Implementations
{
    [CreateAssetMenu(fileName = "ReplaceItemsAbility", menuName = "Configs/Abilities/ReplaceItemsAbility")]
    public class ReplaceItemsAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new ItemsReplacer();
    }
}