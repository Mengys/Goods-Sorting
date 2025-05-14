using System;
using System.Collections.Generic;
using _Project.Code.Data.Dynamic;
using _Project.Code.Gameplay.GridFeature.Services;
using _Project.Code.Gameplay.Items;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Infrastructure.UIRoot;
using _Project.Code.Services.Factories.Item;
using R3;
using UnityEngine;

namespace _Project.Code.Gameplay.GridFeature
{
    public class ItemGrid : IDisposable, IGrid
    {
        private readonly CompositeDisposable _disposable = new();

        private readonly Dictionary<CellGridPosition, ItemPresenter> _items;

        private readonly ItemInventory _itemInventory;
        private readonly CellsMatchHandler _matchCollectedHandler;
        private readonly ItemsDragHandler _dragHandler;
        private readonly LayersActivationHandler _layersActivationHandler;
        private readonly AllMatchesCollectedObserver _allMatchesCollectedObserver;
        private readonly FirstLayerFilledObserver _firstLayerFilledObserver;

        public ItemGrid(List<ShelfPresenter> shelves,
            Dictionary<CellGridPosition, ItemPresenter> items,
            IUIRoot uiRoot, ItemFactory itemFactory)
        {
            _items = items;

            _itemInventory = new ItemInventory(shelves, itemFactory);

            _firstLayerFilledObserver = new FirstLayerFilledObserver(_itemInventory);
            _dragHandler = new ItemsDragHandler(uiRoot.Transform, _itemInventory);
            _matchCollectedHandler = new CellsMatchHandler(_itemInventory);
            _layersActivationHandler = new LayersActivationHandler(_itemInventory);
            _allMatchesCollectedObserver = new AllMatchesCollectedObserver(_itemInventory);
        }

        public Observable<int> MatchCollected => _matchCollectedHandler.MatchHandled;
        public Observable<Unit> AllMatchesCollected => _allMatchesCollectedObserver.Collected;
        public Observable<Unit> FirstLayerFilled => _firstLayerFilledObserver.Filled;

        public Observable<Unit> FirstMoveMade => _itemInventory.ChangedObservable
            .AsUnitObservable()
            .Take(1);

        public ItemInventory ItemInventory => _itemInventory;

        public void Dispose() => _disposable?.Dispose();
        public void Disable() => _dragHandler.Enabled = false;
        public void Enable() => _dragHandler.Enabled = true;

        public void Initialize()
        {
            _itemInventory.ChangedObservable
                .Subscribe(_ =>
                {
                    Debug.Log("Changed");
                    _matchCollectedHandler.Handle();
                    _layersActivationHandler.Handle();
                    _firstLayerFilledObserver.Observe();
                    _allMatchesCollectedObserver.Observe();
                })
                .AddTo(_disposable);

            foreach (var (position, item) in _items)
                _itemInventory.Set(position, item, true);

            foreach (var item in _items.Values)
                _dragHandler.Register(item).AddTo(_disposable);

            _itemInventory.NewItemAdded.Subscribe(item =>
                    _dragHandler.Register(item).AddTo(_disposable))
                .AddTo(_disposable);
        }
    }
}