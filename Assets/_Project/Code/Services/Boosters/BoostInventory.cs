using System;
using System.Collections.Generic;

public class BoostInventory
{
    private Dictionary<Type, int> _boosts = new Dictionary<Type, int>();

    public event Action<Type, int> OnBoostCountChanged;

    public int GetCount<T>() where T : AbilityConfig
    {
        _boosts.TryGetValue(typeof(T), out int count);
        return count;
    }

    public void AddBoost<T>(int amount = 1) where T : AbilityConfig
    {
        Type type = typeof(T);
        if (_boosts.ContainsKey(type))
            _boosts[type] += amount;
        else
            _boosts[type] = amount;

        OnBoostCountChanged?.Invoke(type, _boosts[type]);
    }

    public bool TryUseBoost<T>() where T : AbilityConfig
    {
        Type type = typeof(T);

        if (_boosts.TryGetValue(type, out int count) && count > 0)
        {
            _boosts[type]--;
            OnBoostCountChanged?.Invoke(type, _boosts[type]);
            return true;
        }

        return false;
    }
}
