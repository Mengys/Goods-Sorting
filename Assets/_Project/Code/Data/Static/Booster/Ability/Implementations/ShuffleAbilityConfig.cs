using _Project.Code.Services.BoosterUser.Boosters.Ability;
using _Project.Code.Services.BoosterUser.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability.Implementations
{
    [CreateAssetMenu(fileName = "ShuffleAbility", menuName = "Configs/Abilities/ShuffleAbility")]
    public class ShuffleAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new Shuffler();
    }
}