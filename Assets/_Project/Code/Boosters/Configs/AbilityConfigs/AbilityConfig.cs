using UnityEngine;

public abstract class AbilityConfig : ScriptableObject
{
    public abstract IAbility GetAbility();
}