using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Data.Static.BoosterInventory;
using _Project.Code.Services.BoosterUser;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Elements.Booster.Cell;
using _Project.Code.UI.Elements.Booster.Inventory;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements.Booster.Factory
{
    public class BoosterInventoryFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly IProgressProvider _progress;
        private readonly IWindowFactory _windowFactory;

        public BoosterInventoryFactory(
            IConfigProvider configProvider,
            IProgressProvider progress,
            IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            _progress = progress;
            _configProvider = configProvider;
        }

        public (BoosterInventory, CellsSelector) CreateForMenu(Transform parent)
        {
            var cells = CreateCells(
                _configProvider.MenuBoosterInventoryConfig,
                _configProvider.BoosterCellPrefab,
                parent
            );
            
            var progress = _progress.PlayerProgress.MenuBoosterInventory;
            var inventory = new BoosterInventory(cells, progress).AddTo(parent);
            
            var selector = new CellsSelector(
                cells,
                _windowFactory,
                inventory
            );
            
            return (inventory, selector);
        }

        public void CreateForGameplay(Transform parent, DiContainer context)
        {
            var cells = CreateCells(
                _configProvider.GameplayBoosterInventoryConfig,
                _configProvider.BoosterCellPrefab,
                parent
            );

            var progress = _progress.PlayerProgress.GameplayBoosterInventory;
            var inventory = new BoosterInventory(cells, progress).AddTo(parent);
            
            var user = new CellsUser(
                cells,
                context.Resolve<IBoosterUser>(),
                _windowFactory,
                inventory);
        }
        
        private List<BoosterCell> CreateCells(
            BoosterInventoryConfig? config,
            BoosterCellView prefab,
            Transform parent)
        {
            if (prefab == null)
                throw new Exception("Booster cell prefab not found");
            
            if (config == null)
                throw new Exception("Booster inventory config not found");
            
            var models = new List<BoosterCell>();

            for (var i = 0; i < config.Value.Cells.Count; i++)
            {
                var cellId = config.Value.Cells[i];
                var boosterId = new BoosterId(cellId.ToString());
                var isBlocked = IsBlocked(config.Value, i);

                var model = new BoosterCell(_configProvider, boosterId, 0, isBlocked);
                var view = UnityEngine.Object.Instantiate(prefab, parent);
                var presenter = new BoosterCellPresenter(model, view);

                presenter.Initialize();
                models.Add(model);
            }

            return models;
        }

        private bool IsBlocked(BoosterInventoryConfig config, int cellIndex)
        {
            var passedLevelIndex = _progress.PlayerProgress.Level.Id - 1;

            var blocks = config.Blocks
                .Where(x => x.CellIndex == cellIndex)
                .ToList();

            return blocks.Count > 0 &&
                   blocks.All(x => x.UnblockLevelIndex > passedLevelIndex);
        }
    }
}