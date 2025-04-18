using UnityEngine;

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