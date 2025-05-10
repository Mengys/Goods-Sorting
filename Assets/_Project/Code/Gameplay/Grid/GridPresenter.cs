using System;
using System.Collections.Generic;
using _Project.Code.Gameplay.Grid.Cells;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Infrastructure.UIRoot;
using R3;

namespace _Project.Code.Gameplay.Grid
{
    public class GridPresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable = new();

        private readonly Dictionary<CellGridPosition, ItemPresenter> _items;
        private readonly CellsInventory _cellsInventory;

        private readonly CellsMatchHandler _matchHandler;
        private ItemsDragHandler _dragHandler;
        private readonly LayersActivator _layersActivator;
        private readonly AllMatchesCollectedDetector _collectedDetector;
        private IUIRoot _uiRoot;

        public GridPresenter(
            List<ShelfPresenter> shelves,
            Dictionary<CellGridPosition, ItemPresenter> items,
            IUIRoot uiRoot)
        {
            _uiRoot = uiRoot;
            _items = items;

            _cellsInventory = new CellsInventory(shelves);
            
            _dragHandler = new ItemsDragHandler(uiRoot.Transform, _cellsInventory).AddTo(_disposable);
            _matchHandler = new CellsMatchHandler(_cellsInventory).AddTo(_disposable);
            _layersActivator = new LayersActivator(_cellsInventory).AddTo(_disposable);
            _collectedDetector = new AllMatchesCollectedDetector(_cellsInventory).AddTo(_disposable);

            Initialize();
        }
        
        public Observable<int> MatchHandled => _matchHandler.MatchHandled;
        
        public Observable<Unit> AllMatchesCollected => _collectedDetector.Collected;
        
        public Observable<Unit> FirstMoveMade => _dragHandler.DraggedItem.Take(1);

        private void Initialize()
        {
            foreach (var (position, item) in _items)
                _cellsInventory.Set(position, item);

            foreach (var item in _items.Values) 
                _dragHandler.Add(item);
        }

        public void Dispose() => _disposable?.Dispose();

        public void Pause() => _dragHandler.Disable();

        public void Continue() => _dragHandler.Enable();
    }
}
