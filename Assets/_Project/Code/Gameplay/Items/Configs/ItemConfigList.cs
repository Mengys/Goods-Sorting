using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Gameplay.Items.Configs
{
    [CreateAssetMenu(fileName = "ItemConfigList", menuName = "Configs/ItemConfigList")]
    public class ItemConfigList  : ScriptableObject
    {
        [field: SerializeField] public ItemView ItemPrefab { get; private set; }
        [field: SerializeField] public List<ItemConfig> Configs { get; private set; }
    }
}