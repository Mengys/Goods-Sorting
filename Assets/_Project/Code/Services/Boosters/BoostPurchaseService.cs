using _Project.Code.Gameplay.Boosters.Configs.AbilityConfigs;
using _Project.Code.Gameplay.Wallet;

namespace _Project.Code.Services.Boosters
{
    public class BoostPurchaseService
    {
        private Wallet _wallet;
        private BoostInventory _inventory;

        public BoostPurchaseService(Wallet wallet, BoostInventory inventory)
        {
            _wallet = wallet;
            _inventory = inventory;
        }

        public void TryPurchaseBoost<T>() where T : AbilityConfig
        {

        }
    }
}
