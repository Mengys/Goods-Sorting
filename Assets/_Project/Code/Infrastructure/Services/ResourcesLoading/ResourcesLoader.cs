using UnityEngine;

namespace _Project.Code.Infrastructure.Services.ResourcesLoading
{
    public class ResourcesLoader
    {
        public T LoadResource<T>(string resourcePath) where T : Object
            => Resources.Load<T>(resourcePath);
    }
}