using System.Collections.Generic;
using _Project.Code.Gameplay.Shelfs;
using UnityEngine;

[CreateAssetMenu(fileName = "ReplaceObjectsAbility", menuName = "Configs/Abilities/ ReplaceObjectsAbility")]
public class ReplaceObjectsAbilityConfig : AbilityConfig
{
    private readonly List<Shelf> Shelves;

    private int _countObjectsReplace = 6;

    public override IAbility GetAbility()
    {
        ReplaceObjects replaceObjects = new ReplaceObjects(Shelves, _countObjectsReplace);

        return replaceObjects;
    }
}