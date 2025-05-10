using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textTimer;

        public void UpdateView(float seconds) =>
            _textTimer.text = seconds.ToString("0");
    }
}
