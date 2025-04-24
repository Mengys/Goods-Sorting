using UnityEngine;
using Zenject;

namespace _Project.Code.Architecture.Services.LevelGeneration
{
    public class LevelGenerator
    {
        [Inject] private ConfigProvider _configProvider;
        [Inject] private ResourcesLoader _resourcesLoader;
        
        public void Generate()
        {
            var prefab = _resourcesLoader.LoadResource<GameObject>(ResourcesPaths.ShelfPrefab);
            var config = _configProvider.LevelsConfigProvider.Configs[0];
            
            foreach (var shelf in config.Shelves)
            {
                var position = shelf.Position;
                
                var instance = Object.Instantiate(prefab, position, Quaternion.identity);
            }
        }
    }
}