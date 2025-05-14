using _Project.Code.Data.Static.Shelf;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Services.ConfigProvider;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.Factories.Shelf
{
    public class ShelfFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _container;

        public ShelfFactory(IConfigProvider configProvider, DiContainer container)
        {
            _container = container;
            _configProvider = configProvider;
        }

        public ShelfView Create(ShelfId id, Vector3 position, Transform parent = null)
        {
            var prefab = _configProvider.ForShelf(id).Value.Prefab;

            ShelfView instance = 
                _container.InstantiatePrefabForComponent<ShelfView>(prefab);

            instance.transform.SetParent(parent, false);
        
            instance.Initialize();
        
            instance.Position = position;
        
            return instance;
        }
    }
}