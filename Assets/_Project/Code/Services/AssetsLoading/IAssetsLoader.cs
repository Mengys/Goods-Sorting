using UnityEngine;

namespace _Project.Code.Services.AssetsLoading
{
    public interface IAssetsLoader
    {
        T Load<T>(string resourcePath) where T : Object;
    }
}