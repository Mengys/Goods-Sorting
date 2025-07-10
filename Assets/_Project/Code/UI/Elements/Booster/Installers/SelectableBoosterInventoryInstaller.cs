using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Elements.Booster.Factory;
using _Project.Code.UI.Elements.Booster.Inventory;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements.Booster.Installers
{
    public class SelectableBoosterInventoryInstaller : MonoBehaviour
    {
        private IProgressProvider _progress;
        private BoosterInventoryFactory _inventoryFactory;

        private CellsSelector _selector;
        private IBoosterInventory _inventory;

        [Inject]
        public void Construct(BoosterInventoryFactory factory)
        {
            _inventoryFactory = factory;
        }

        private void Awake()
        {
             (_inventory, _selector) = _inventoryFactory.CreateForMenu(transform);
        }

        public BoosterId? GetBooster()
        {
            if (SelectedBoosterId.HasValue)
                _inventory.Remove(SelectedBoosterId.Value);

            return SelectedBoosterId;
        }

        private BoosterId? SelectedBoosterId => _selector.SelectedBoosterId;
    }
}