using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfigProvider", menuName = "Configs/AbilityConfigProvider")]
public class AbilityConfigProvider : ScriptableObject
{
    [SerializeField] private List<AbilityConfig> _abilities = new List<AbilityConfig>();
}