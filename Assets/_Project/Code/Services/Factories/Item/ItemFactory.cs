using _Project.Code.Gameplay.Items;
using _Project.Code.Services.ConfigProvider;
using UnityEngine;
using Zenject;

namespace _Project.Code.Services.Factories.Item
{
    public class ItemFactory
    {
        private readonly IConfigProvider _configProvider;
        private readonly DiContainer _container;

        public ItemFactory(IConfigProvider configProvider, DiContainer container)
        {
            _configProvider = configProvider;
            _container = container;
        }

        public ItemPresenter Create(ItemId id, Vector3 position, Transform parent = null)
        {
            var prefab = _configProvider.PrefabForItem(id);

            var instance = CreateView(prefab, id, position, parent);

            return new ItemPresenter(id, instance);
        }

        public ItemView CreatePreview(ItemId id, Vector3 position, Transform parent = null)
        {
            var prefab = _configProvider.PreviewPrefabForItem(id);

            return CreateView(prefab, id, position, parent);
        }

        private ItemView CreateView(ItemView prefab, ItemId id, Vector3 position, Transform parent = null)
        {
            var config = _configProvider.ForItem(id).Value;

            var instance =
                _container.InstantiatePrefabForComponent<ItemView>(prefab, position, Quaternion.identity, parent);

            instance.UpdateView(config);

            return instance;
        }
    }
}