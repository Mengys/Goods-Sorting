using UnityEngine;

public abstract class AbilityConfig : ScriptableObject
{
    public abstract IAbility GetAbility(); 
}

public class TimeStopAbilityConfig : AbilityConfig
{
    int _delay = 5;

    public override IAbility GetAbility()
    {
        StopTimer stopTimer = new StopTimer(_delay);

        return stopTimer;
    }
}
