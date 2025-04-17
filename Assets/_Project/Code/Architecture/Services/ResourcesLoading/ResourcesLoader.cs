using UnityEngine;

namespace _Project.Code.Architecture
{
    public class ResourcesLoader
    {
        public T LoadResource<T>(string resourcePath) where T : Object
            => Resources.Load<T>(resourcePath);
    }
}