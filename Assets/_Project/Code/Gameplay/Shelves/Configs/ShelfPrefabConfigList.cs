using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Shelves.Configs
{
    [CreateAssetMenu(fileName = nameof(ShelfPrefabConfigList), menuName = "Configs/" + nameof(ShelfPrefabConfigList))]
    public class ShelfPrefabConfigList : ScriptableObject
    {
        [field: SerializeField] public List<ShelfPrefabConfig> Shelves { get; set; }
    }
}