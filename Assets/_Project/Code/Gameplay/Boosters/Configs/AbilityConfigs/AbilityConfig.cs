using _Project.Code.Gameplay.Boosters.Ability;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    public abstract class AbilityConfig : ScriptableObject
    {
        public abstract IAbility GetAbility();
    }
}