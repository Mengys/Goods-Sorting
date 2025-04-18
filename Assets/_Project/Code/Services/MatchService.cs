using System;
using System.Collections.Generic;

public class MatchService : IDisposable
{
    private Score _wallet;
    private List<Shelf> _shelves = new List<Shelf>();

    public MatchService(Score wallet, List<Shelf> shelves)
    {
        _wallet = wallet;
        _shelves = shelves;

        foreach (var shelf in _shelves)
        {
            shelf.Matched += _wallet.AddScore;
        }
    }

    public void Dispose()
    {
        foreach (var shelf in _shelves)
        {
            shelf.Matched -= _wallet.AddScore;
        }
    }
}
