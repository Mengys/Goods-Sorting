using System;

public class WalletPresenter : IDisposable
{
    private Wallet _wallet;
    private WalletView _walletView;

    public WalletPresenter(Wallet wallet, WalletView walletView)
    {
        _wallet = wallet;
        _walletView = walletView;

        _wallet.Changed += _walletView.UpdateView;
        _walletView.UpdateView(_wallet.CurrentMoney);
    }

    public void Dispose()
    {
        _wallet.Changed -= _walletView.UpdateView;
    }
}
