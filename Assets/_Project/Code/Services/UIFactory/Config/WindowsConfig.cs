using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Services.UIFactory.Config
{
    [CreateAssetMenu(fileName = "WindowsConfig", menuName = "Configs/WindowsConfig")]
    public class WindowsConfig : ScriptableObject
    {
        [field: SerializeField] public List<WindowConfig> Windows { get; private set; }
    }
}