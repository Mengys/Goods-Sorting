using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "TimeStopAbility", menuName = "Configs/Abilities/ TimeStopAbility")]
    public class TimeStopAbilityConfig : AbilityConfig
    {
        int _delay = 5;

        public override IAbility GetAbility()
        {
            StopTimer stopTimer = new StopTimer(_delay);

            return stopTimer;
        }
    }
}