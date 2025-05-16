using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.Boosters.Ability;
using _Project.Code.Gameplay.Counter;
using _Project.Code.Gameplay.GridFeature;
using _Project.Code.Gameplay.Items;
using _Project.Code.UI.Buttons.Window;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Boosters.Boosters
{
    public class Bomb : IAbility
    {
        private IGrid _grid;
        private IItemCollectHandler _itemCollectHandler;

        public void Initialize(DiContainer container)
        {
            _itemCollectHandler = container.Resolve<IItemCollectHandler>();
            _grid = container.Resolve<IGrid>();
        }

        public void Use()
        {
            List<ItemId> ids = GetIds();
            
            if (ids.Count == 0) return;
            
            ItemId randomId = PickRandomId(ids);
            List<CellGridPosition> positions = GetPositionsBy(randomId);

            Collect(positions);
        }

        private List<ItemId> GetIds()
        {
            return _grid.ItemInventory.Cells
                .Where(pair => pair.Value != null)
                .Select(pair => pair.Value.Id)
                .Distinct()
                .ToList();
        }

        private ItemId PickRandomId(List<ItemId> ids) => 
            ids[Random.Range(0, ids.Count)];

        private List<CellGridPosition> GetPositionsBy(ItemId randomId)
        {
            return _grid.ItemInventory.Cells
                .Where(pair => pair.Value?.Id.Equals(randomId) == true)
                .Select(pair => pair.Key)
                .ToList();
        }

        private void Collect(List<CellGridPosition> positions)
        {
            positions.ForEach(_grid.ItemInventory.Clear);
            _itemCollectHandler.Handle(positions.Count);
        }
    }
}