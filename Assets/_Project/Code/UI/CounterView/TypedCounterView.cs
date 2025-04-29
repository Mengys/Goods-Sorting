using UnityEngine;

namespace _Project.Code.UI.CounterView
{
    public class TypedCounterView : CounterView
    {
        [field: SerializeField] public CounterType CounterType { get; private set; }
    }
}