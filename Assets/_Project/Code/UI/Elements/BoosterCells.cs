using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.UI.Elements.Booster;

namespace _Project.Code.UI.Elements
{
    public class BoosterCellsPresenter
    {
        
    }

    public class BoosterCells
    {
        public List<BoosterCell> Cells { get; } = new();

        public void Set(List<BoosterCell> cells)
        {
            Cells.Clear();
            
            foreach (var cell in cells)
                Cells.Add(cell);
        }

        public void LockCell(int index) =>
            SetCellLocked(index, true);
        
        public void UnlockCell(int index) =>
            SetCellLocked(index, false);
        
        public BoosterCell? GetCellBy(BoosterId id)
        {
            var index = 0;
            //Cells.FindIndex(cell => cell.Id == id);

            return index == -1 ? null : Cells[index];
        }

        private void SetCellLocked(int index, bool value)
        {
            if (index < 0 || index >= Cells.Count)
                throw new Exception("Index out of range");

            var cell = Cells[index];
           // cell.IsBlocked = value;
            Cells[index] = cell;
        }
    }
}