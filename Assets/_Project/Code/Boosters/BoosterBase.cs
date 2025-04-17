using UnityEngine;

public abstract class BoosterBase : MonoBehaviour
{
    protected BoosterConfig _config;
    protected Wallet _wallet;

    public int Price => _config._price;

    public void Initialize(BoosterConfig config, Wallet wallet)
    {
        _config = config;
        _wallet = wallet;
    }

    public bool CanUse(Wallet wallet) => wallet.CurrentMoney >= Price;

    public abstract void Use();
}
