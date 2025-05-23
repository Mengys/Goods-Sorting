using UnityEngine;

namespace _Project.Code.Services.AssetsLoading
{
    public class AssetLoader : IAssetsLoader
    {
        public T Load<T>(string resourcePath) where T : Object
            => Resources.Load<T>(resourcePath);
    }
}