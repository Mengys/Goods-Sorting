using UnityEngine;
using Zenject;

namespace _Project.Code.UI.UIRoot
{
    public interface IUIRoot
    {
        Transform Transform { get; }
        DiContainer Container { get; }
    }
}