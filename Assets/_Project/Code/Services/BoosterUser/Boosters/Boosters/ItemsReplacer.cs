using System.Collections.Generic;
using System.Linq;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.Items;
using _Project.Code.Services.BoosterUser.Boosters.Ability;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.BoosterUser.Boosters.Boosters
{
    public class ItemsReplacer : IAbility
    {
        private IGrid _grid;
        private readonly int _replaceCount;

        public ItemsReplacer(int replaceCount = 5)
        {
            _replaceCount = replaceCount;
        }

        public void Initialize(DiContainer container)
        {
            _grid = container.Resolve<IGrid>();
        }

        public void Use()
        {
            var cells = 
                _grid.ItemInventory.Cells.Where(pair => pair.Value != null)
                    .Select(pair => pair.Key)
                    .ToList();
            
            ShuffleFisherYates(cells);
            
            var replaceCount = Mathf.Min(_replaceCount, cells.Count);
            var randomId = PickRandomItemId();

            for (int i = 0; i < replaceCount; i++)
            {
                _grid.ItemInventory.Replace(cells[i], randomId);
            }
        }

        private ItemId PickRandomItemId()
        {
            var ids = _grid.ItemInventory.Cells
                .Where(pair => pair.Value != null)
                .Select(pair => pair.Value.Id)
                .Distinct()
                .ToList();

            return ids[Random.Range(0, ids.Count)];
        }

        private List<T> ShuffleFisherYates<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }

            return list;
        }
    }
}
