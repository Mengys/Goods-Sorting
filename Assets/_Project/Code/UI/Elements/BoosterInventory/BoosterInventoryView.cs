using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Static.Booster;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Code.UI.Elements
{
    public class BoosterInventoryView : MonoBehaviour
    {
        [SerializeField] private BoosterCellView _cellPrefab;

        public void Draw(IEnumerable<BoosterCell> inventoryCells)
        {
        }
    }

    public struct BoosterCell
    {
        public bool IsLocked;
        public bool IsEmpty => Id == default;

        public BoosterId Id;
        public int Count;
    }

    public class BoosterCellView : MonoBehaviour
    {
        [SerializeField] private Image _icon;

        public void Draw()
        {
        }
    }
}