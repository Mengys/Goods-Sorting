using _Project.Code.Services.BoosterUser.Boosters.Ability;
using _Project.Code.Services.BoosterUser.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability.Implementations
{
    [CreateAssetMenu(fileName = "TimeStopAbility", menuName = "Configs/Abilities/TimeStopAbility")]
    public class TimeStopperAbilityConfig : AbilityConfig
    {
        public override IAbility GetAbility() => new TimerStopper();
    }
}