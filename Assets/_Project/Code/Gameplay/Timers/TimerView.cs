using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textTimer;

    public void ShowSeconds(float seconds) => _textTimer.text = seconds.ToString();
}
