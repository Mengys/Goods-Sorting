using System;

public class Wallet
{
    private int _money = 0;

    public event Action<int> Changed;

    public int CurrentMoney => _money;

    public void AddMoney(int money)
    {
        _money += money;
        Changed?.Invoke(_money);
    }

    public void RemoveMoney(int money)
    {
        _money -= money;
        Changed?.Invoke(_money);
    }
}
