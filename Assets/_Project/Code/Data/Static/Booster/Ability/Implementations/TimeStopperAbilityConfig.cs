using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "TimeStopAbility", menuName = "Configs/Abilities/TimeStopAbility")]
    public class TimeStopperAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new TimerStopper();
    }
}