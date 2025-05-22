using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.UI.Buttons.Booster;
using _Project.Code.UI.Elements.Booster;
using UnityEngine;
using R3;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class SelectableBoosterInventory
    {
        private BoosterCell _selectedCell;

        public SelectableBoosterInventory(List<BoosterCell> cells)
        {
            cells.ForEach(cell => cell.Clicked.Subscribe(HandleCellClick));
        }

        private void HandleCellClick(BoosterCell cell)
        {
            Debug.Log("Cell clicked");

            if (_selectedCell == cell)
            {
                cell.IsSelected.Value = false;
                _selectedCell = null;
                return;
            }

            if (_selectedCell != null)
                _selectedCell.IsSelected.Value = false;

            _selectedCell = cell;
            _selectedCell.IsSelected.Value = true;
        }
    }

    public class UsableBoosterInventory : IDisposable
    {
        private BoosterCell _selectedCell;

        private readonly IBoosterUser _user;
        private readonly CompositeDisposable _disposable = new();

        public UsableBoosterInventory(List<BoosterCell> cells, IBoosterUser user)
        {
            _user = user;
            cells.ForEach(cell => cell.Clicked.Subscribe(HandleCellClick).AddTo(_disposable));
        }

        private void HandleCellClick(BoosterCell cell)
        {
            _user.Use(cell.Id.Value);
        }

        public void Dispose() => _disposable?.Dispose();
    }

    public class BoosterInventoryFactory
    {
        private readonly IConfigProvider _configProvider;

        public BoosterInventoryFactory(IConfigProvider configProvider)
        {
            _configProvider = configProvider;
        }

        public void CreateForMenu(Transform parent)
        {
            CreateInventory(
                _configProvider.MenuBoosterInventoryConfig,
                parent,
                cells => new SelectableBoosterInventory(cells)
            );
        }

        public void CreateForGameplay(Transform parent, DiContainer context)
        {
            CreateInventory(
                _configProvider.GameplayBoosterInventoryConfig,
                parent,
                cells =>
                    new UsableBoosterInventory(cells, context.Resolve<IBoosterUser>())
            );
        }

        private T CreateInventory<T>(
            BoosterInventoryConfig? config,
            Transform parent,
            Func<List<BoosterCell>, T> inventoryFactory)
        {
            var prefab = _configProvider.BoosterCellPrefab
                         ?? throw new Exception("Booster cell prefab not found");

            if (config == null)
                throw new Exception("Booster inventory config not found");

            var cells = CreateCells(config.Value, prefab, parent);
            var inventory = inventoryFactory(cells);

            return inventory;
        }

        private List<BoosterCell> CreateCells(
            BoosterInventoryConfig config,
            BoosterCellView prefab,
            Transform parent)
        {
            var models = new List<BoosterCell>();

            foreach (var cellId in config.Cells)
            {
                var view = UnityEngine.Object.Instantiate(prefab, parent);
                var boosterId = new BoosterId(cellId.ToString());
                var model = new BoosterCell(_configProvider, boosterId, 0);
                var presenter = new BoosterCellPresenter(model, view);

                presenter.Initialize();
                models.Add(model);
            }

            return models;
        }
    }
}