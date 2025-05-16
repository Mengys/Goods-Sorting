using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.Items;
using _Project.Code.UI.Buttons.Window;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class ComboCollector : IAbility
    {
        private IGrid _grid;
        private readonly int _numberObjectsSameType;
        private IItemCollectHandler _itemCollectHandler;

        public void Initialize(DiContainer container)
        {
            _itemCollectHandler = container.Resolve<IItemCollectHandler>();
            _grid = container.Resolve<IGrid>();
        }

        public void Use()
        {
            var positionsByItemId = _grid.ItemInventory
                .Cells
                .Where(pair => pair.Value != null)
                .GroupBy(pair => pair.Value.Id)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(pair => pair.Key).ToList()
                );

            ItemId mostFrequentItemId = positionsByItemId
                    .OrderByDescending(kvp => kvp.Value.Count)
                    .First()
                    .Key;
            
            Collect(positionsByItemId[mostFrequentItemId]);
        }

        private void Collect(List<CellGridPosition> cells)
        {
            cells.ForEach(_grid.ItemInventory.Clear);
            _itemCollectHandler.Handle(cells.Count);
        }
    }
}