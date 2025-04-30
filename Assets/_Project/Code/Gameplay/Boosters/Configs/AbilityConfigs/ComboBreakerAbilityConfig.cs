using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboBreakerAbility", menuName = "Configs/Abilities/ComboBreakerAbility")]
public class ComboBreakerAbilityConfig : AbilityConfig
{
    private readonly List<Shelf> Shelves;

    private int _numberObjectsSameType = 3;

    public override IAbility GetAbility()
    {
        ComboBreaker comboBreaker = new ComboBreaker(Shelves, _numberObjectsSameType);

        return comboBreaker;
    }
}