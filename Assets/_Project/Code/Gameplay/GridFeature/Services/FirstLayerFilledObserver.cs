using System.Linq;
using R3;

namespace _Project.Code.Gameplay.GridFeature.Services
{
    public class FirstLayerFilledObserver
    {
        private readonly ItemInventory _itemInventory;
        private readonly Subject<Unit> _filled = new();

        public FirstLayerFilledObserver(ItemInventory itemInventory)
        {
            _itemInventory = itemInventory;
        }

        public Observable<Unit> Filled => _filled;

        public void Observe()
        {
            var cells = _itemInventory.Cells;

            var firstLayerPositions = cells.Keys
                .Where(pos => pos.Layer == 0)
                .ToList();

            bool allFilled = firstLayerPositions
                .All(pos => cells[pos] != null);

            if (allFilled) 
                _filled.OnNext(Unit.Default);
        }
    }
}