using _Project.Code.Services.BoosterUser.Boosters.Ability;
using _Project.Code.Services.BoosterUser.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability.Implementations
{
    [CreateAssetMenu(fileName = "BombAbility", menuName = "Configs/Abilities/BombAbility")]
    public class BombAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new Bomb();
    }
}