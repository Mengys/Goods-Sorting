using UnityEngine;

namespace _Project.Code.Gameplay.Shelves
{
    public class ShelfPresenter
    {
        private readonly ShelfView _view;
    
        public ShelfPresenter(ShelfView view, int layersCount, int columnsCount)
        {
            _view = view;
        
            LayersCount = layersCount;
            ColumnsCount = columnsCount;
        }
    
        public void PlaceItem(Transform item, int column, int layer) => 
            _view.PlaceItem(item, layer, column);
        public void MoveItem(Transform item, int column, int layer) => 
            _view.MoveItem(item, layer, column);
    
        public Vector3? GetCellPosition(int column, int layer = 0) => 
            _view.GetCellPosition(column, layer);

        public int ColumnsCount { get; }
        public int LayersCount { get; }
    }
}