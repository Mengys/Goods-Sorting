using System.Collections.Generic;
using _Project.Code.Gameplay.Items;
using UnityEngine;

namespace _Project.Code.Data.Static.Item
{
    [CreateAssetMenu(fileName = "ItemConfigList", menuName = "Configs/Lists/ItemConfigList")]
    public class ItemConfigList  : ScriptableObject
    {
        [field: SerializeField] public ItemView ItemPrefab { get; private set; }
        [field: SerializeField] public List<ItemConfig> Configs { get; private set; }
    }
}