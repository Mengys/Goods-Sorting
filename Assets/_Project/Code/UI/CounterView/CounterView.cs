using TMPro;
using UnityEngine;

namespace _Project.Code.UI.CounterView
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counter;
            
        public void SetCounter(int value) =>
            _counter.text = value.ToString();
    }
}