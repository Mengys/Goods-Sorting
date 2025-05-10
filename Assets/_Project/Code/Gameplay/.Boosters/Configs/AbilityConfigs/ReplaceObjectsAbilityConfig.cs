using System.Collections.Generic;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Boosters.Boosters;
using _Project.Code.Gameplay.Shelfs;
using UnityEngine;

namespace _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs
{
    [CreateAssetMenu(fileName = "ReplaceObjectsAbility", menuName = "Configs/Abilities/ ReplaceObjectsAbility")]
    public class ReplaceObjectsAbilityConfig : AbilityConfig
    {
        private readonly List<Shelfs.Shelf> Shelves;

        private int _countObjectsReplace = 6;

        public override IAbility GetAbility()
        {
            ReplaceObjects replaceObjects = new ReplaceObjects(Shelves, _countObjectsReplace);

            return replaceObjects;
        }
    }
}