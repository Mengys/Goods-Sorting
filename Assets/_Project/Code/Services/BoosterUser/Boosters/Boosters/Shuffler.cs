using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.Items;
using _Project.Code.Services.BoosterUser.Boosters.Ability;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.BoosterUser.Boosters.Boosters
{
    public class Shuffler : IAbility
    {
        private IGrid _grid;

        public void Initialize(DiContainer container)
        {
            _grid = container.Resolve<IGrid>();
        }

        public void Use() =>
            Shuffle(_grid.ItemInventory.Cells);

        private void Shuffle(IReadOnlyDictionary<CellGridPosition, ItemPresenter> cells)
        {
            if (cells.Count < 2) return;

            var positions = cells.Keys.ToList();
            var newPositions = GetFisherYatesShuffled(positions.ToList());

            Debug.Log($"Shuffling {positions.Count} items");

            for (int i = 0; i < positions.Count; i++)
            {
                var current = positions[i];
                var target = newPositions[i];
                
                _grid.ItemInventory.Swap(current, target);
            }
        }

        private List<T> GetFisherYatesShuffled<T>(List<T> list)
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