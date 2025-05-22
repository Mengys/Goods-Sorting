using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.BoosterUser;
using _Project.Code.Services.Factories.UI;
using _Project.Code.UI.Elements.Booster.Cell;
using R3;
using UnityEngine;

namespace _Project.Code.UI.Elements.Booster.Inventory
{
    public class CellsUser : IDisposable
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IBoosterUser _boosterUser;
        private readonly IBoosterInventory _inventory;

        private readonly CompositeDisposable _disposable = new();

        public CellsUser(
            List<BoosterCell> cells,
            IBoosterUser boosterUser,
            IWindowFactory windowFactory,
            IBoosterInventory inventory)
        {
            _inventory = inventory;
            _windowFactory = windowFactory;
            _boosterUser = boosterUser;

            cells.ForEach(cell =>
                cell.Clicked.Subscribe(Handle)
                    .AddTo(_disposable));
        }

        private void Handle(BoosterCell cell)
        {
            if (cell.Count.Value < 1)
            {
                _windowFactory.CreateBuyBooster(cell.Id.Value, _inventory);
                return;
            }

            cell.Count.Value--;

            _boosterUser.Use(cell.Id.Value);
        }

        public void Dispose() => _disposable?.Dispose();
    }

    public class CellsSelector : IDisposable
    {
        private BoosterCell _selectedCell;
        
        private readonly IWindowFactory _windowFactory;
        private readonly IBoosterInventory _inventory;
        
        private readonly CompositeDisposable _disposable = new();

        public CellsSelector(
            List<BoosterCell> cells,
            IWindowFactory windowFactory,
            IBoosterInventory inventory)
        {
            _inventory = inventory;
            _windowFactory = windowFactory;

            cells.ForEach(cell =>
                cell.Clicked.Subscribe(Handle)
                    .AddTo(_disposable));
        }
        
        public BoosterId? SelectedBoosterId => _selectedCell?.Id.Value;
        
        public void Dispose() => _disposable?.Dispose();
        
        private void Handle(BoosterCell cell)
        {
            if (_selectedCell == cell)
            {
                cell.IsSelected.Value = false;
                _selectedCell = null;
                return;
            }

            if (cell.Count.Value < 1)
            {
                _windowFactory.CreateBuyBooster(cell.Id.Value, _inventory);
                return;
            }

            if (_selectedCell != null)
                _selectedCell.IsSelected.Value = false;

            _selectedCell = cell;
            _selectedCell.IsSelected.Value = true;
        }
    }

    public class BoosterInventory : IDisposable, IBoosterInventory
    {
        private readonly Dictionary<BoosterId, int> _boosters;
        private readonly List<BoosterCell> _cells;

        private readonly CompositeDisposable _disposable = new();

        public BoosterInventory(
            List<BoosterCell> cells,
            BoosterInventoryData progress)
        {
            _cells = cells;
            _boosters = progress.Boosters;

            OnInventoryChanged();

            cells.ForEach(cell =>
                cell.Count.Subscribe(cellCount =>
                        _boosters[cell.Id.Value] = cellCount)
                    .AddTo(_disposable));
        }
        
        public void Dispose() =>
            _disposable?.Dispose();

        public void Add(BoosterId id)
        {
            _boosters[id] = _boosters.GetValueOrDefault(id) + 1;
            OnInventoryChanged();
        }

        public bool Has(BoosterId id) =>
            _boosters.TryGetValue(id, out var count) && count > 0;

        public void Remove(BoosterId id)
        {
            if (_boosters.TryGetValue(id, out var count))
            {
                _boosters[id] = Mathf.Max(0, count - 1);
                OnInventoryChanged();
            }
        }

        private void OnInventoryChanged()
        {
            foreach (var cell in _cells)
            {
                var count = _boosters.GetValueOrDefault(cell.Id.Value);
                cell.Count.Value = count;
            }
        }
    }
}