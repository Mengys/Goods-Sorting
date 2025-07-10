using _Project.Code.Services.BoosterUser.Boosters.Ability;
using _Project.Code.Services.BoosterUser.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability.Implementations
{
    [CreateAssetMenu(fileName = "ComboCollectAbility", menuName = "Configs/Abilities/ComboCollectAbility")]
    public class ComboCollectAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new ComboCollector();
    }
}