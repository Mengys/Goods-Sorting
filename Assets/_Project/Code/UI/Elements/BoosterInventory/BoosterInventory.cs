using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Static.Booster;
using R3;

namespace _Project.Code.UI.Elements
{
    public class BoosterInventory
    {
        private readonly Subject<Unit> _changed = new();
        private readonly IBoosterCells _cells;

        public BoosterInventory(IBoosterCells cells)
        {
            _cells = cells;
        }

        public IReadOnlyList<BoosterCell> Cells => _cells.Cells.ToList();
        public Observable<Unit> Changed => _cells.Cells.ToUnitObservable();

        public void Add(BoosterId id)
        {
            var cell = GetCellIndexBy(id);

            if (cell == -1)
                throw new Exception("Cell not found");

            _changed.OnNext(Unit.Default);
        }

        public bool Has(BoosterId id)
        {
            var cellIndex = GetCellIndexBy(id);

            if (cellIndex == -1) return false;

            return cell != -1 && _cells[cell].Count > 0;
        }

        public void Remove(BoosterId id)
        {
        }
    }
}