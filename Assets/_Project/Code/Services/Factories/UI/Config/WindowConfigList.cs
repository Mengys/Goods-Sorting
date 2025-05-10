using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Services.Factories.UI.Config
{
    [CreateAssetMenu(fileName = nameof(WindowConfigList), menuName = "Configs/" + nameof(WindowConfigList))]
    public class WindowConfigList : ScriptableObject
    {
        [field: SerializeField] public List<WindowConfig> Windows { get; private set; }
    }
}