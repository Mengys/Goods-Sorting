using R3;

namespace _Project.Code.UI.Elements
{
    public class BoosterInventoryPresenter
    {
        private BoosterInventory _inventory;
        private BoosterInventoryView _view;

        public BoosterInventoryPresenter(
            BoosterInventory inventory,
            BoosterInventoryView view)
        {
            _inventory = inventory;
            _view = view;
        }

        public void Initialize()
        {
            _inventory.Changed
                .Subscribe(_ => _view.Draw(_inventory.Cells))
                .AddTo(_view);
        }
    }
}