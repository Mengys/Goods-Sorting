using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.Shelf
{
    [CreateAssetMenu(fileName = nameof(ShelfPrefabConfigList), menuName = "Configs/Lists/" + nameof(ShelfPrefabConfigList))]
    public class ShelfPrefabConfigList : ScriptableObject
    {
        [field: SerializeField] public List<ShelfPrefabConfig> Shelves { get; set; }
    }
}