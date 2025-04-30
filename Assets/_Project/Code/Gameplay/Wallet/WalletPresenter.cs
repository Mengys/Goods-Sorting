namespace _Project.Code.Gameplay.Wallet
{
    public class WalletPresenter 
    {
        private Wallet _wallet;
        private WalletView _walletView;

        public WalletPresenter(Wallet wallet, WalletView waletView)
        {
            _wallet = wallet;
            _walletView = waletView;

            _wallet.Changed += _walletView.UpdateVievMoney;
        }
    }
}
