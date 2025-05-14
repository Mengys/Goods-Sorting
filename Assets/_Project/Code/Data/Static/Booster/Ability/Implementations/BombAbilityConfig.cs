using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "BombAbility", menuName = "Configs/Abilities/BombAbility")]
    public class BombAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new Bomb();
    }
}