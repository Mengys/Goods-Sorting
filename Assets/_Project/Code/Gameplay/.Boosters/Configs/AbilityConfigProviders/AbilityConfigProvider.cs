using System.Collections.Generic;
using _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigProviders
{
    [CreateAssetMenu(fileName = "AbilityConfigProvider", menuName = "Configs/AbilityConfigProvider")]
    public class AbilityConfigProvider : ScriptableObject
    {
        [SerializeField] private List<AbilityConfig> _abilities = new List<AbilityConfig>();

        public AbilityConfig GetAbilityConfig<T>() where T : AbilityConfig
        {
            foreach (var ability in _abilities)
            {
                if (ability is T typedAbility)
                {
                    return typedAbility;
                }
            }

            return null;
        }
    }
}