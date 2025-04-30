using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.UIRoot
{
    public interface IUIRoot
    {
        Transform Transform { get; }
        DiContainer Container { get; }
    }
}