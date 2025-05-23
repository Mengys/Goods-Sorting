using System.Collections.Generic;
using UnityEngine;

namespace _Project.Code.Data.Static.Windows
{
    [CreateAssetMenu(fileName = nameof(WindowConfigList), menuName = "Configs/Lists/" + nameof(WindowConfigList))]
    public class WindowConfigList : ScriptableObject
    {
        [field: SerializeField] public List<WindowConfig> Windows { get; private set; }
    }
}