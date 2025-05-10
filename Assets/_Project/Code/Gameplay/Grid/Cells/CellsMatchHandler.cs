using System;
using System.Collections.Generic;
using System.Linq;
using R3;

namespace _Project.Code.Gameplay.Grid.Cells
{
    public class CellsMatchHandler : IDisposable
    {
        private readonly CellsInventory _cellsInventory;
        private readonly Subject<int> _matchHandled = new();
        
        private readonly CompositeDisposable _disposable = new();
        
        public CellsMatchHandler(CellsInventory cellsInventory)
        {
            _cellsInventory = cellsInventory;
            _cellsInventory.ChangedObservable
                .Subscribe(_ => OnCellsChanged())
                .AddTo(_disposable);
        }

        public Observable<int> MatchHandled => _matchHandled;
        
        private void OnCellsChanged()
        {
            //Debug.Log("Cells changed");

            var itemPositions = _cellsInventory.Cells.Keys;

            var shelfGroups =
                itemPositions.GroupBy(p => p.Shelf);

            foreach (var group in shelfGroups)
            {
                var layerGroups = group.GroupBy(p => p.Layer);

                foreach (var layerGroup in layerGroups)
                {
                    var positions = layerGroup.ToList();
                    
                    var items = positions
                        .Select(p => _cellsInventory.Get(p)).ToList();
                    
                    bool match = items.All(i => i?.Id.Equals(items[0].Id) == true);

                    if (match)
                        OnMatchFound(positions);
                }
            }
        }

        private void OnMatchFound(List<CellGridPosition> positions)
        {
            foreach (var position in positions) 
                _cellsInventory.Clear(position);
            
            _matchHandled?.OnNext(positions.Count);
        }

        public void Dispose()
        {
            _disposable.Dispose();
            _matchHandled.Dispose();
        }
    }
}