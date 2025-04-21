using System;

public class Wallet
{
    private int _currentMoney = 0;

    public event Action<int> Changed;

    public int CurrentScore => _currentMoney;

    public void AddMoney(int money)
    {
        _currentMoney += money;
        Changed?.Invoke(_currentMoney);
    }

    public void RemoveMoney(int money)
    {
        _currentMoney -= money;
        Changed?.Invoke(_currentMoney);
    }
}
