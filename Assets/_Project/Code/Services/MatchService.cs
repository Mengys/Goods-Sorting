using System;
using System.Collections.Generic;

public class MatchService : IDisposable
{
    private Wallet _wallet;
    private List<Shelf> _shelves = new List<Shelf>();

    public MatchService(Wallet wallet, List<Shelf> shelves)
    {
        _wallet = wallet;
        _shelves = shelves;

        foreach (var shelf in _shelves)
        {
            shelf.Matched += _wallet.AddMoney;
        }
    }

    public void Dispose()
    {
        foreach (var shelf in _shelves)
        {
            shelf.Matched -= _wallet.AddMoney;
        }
    }
}
