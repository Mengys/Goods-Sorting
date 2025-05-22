using _Project.Code.Services.BoosterUser.Boosters.Ability;
using UnityEngine;

namespace _Project.Code.Data.Static.Booster.Ability
{
    public abstract class AbilityConfig : ScriptableObject
    {
        public abstract IAbility GetAbility();
    }
}