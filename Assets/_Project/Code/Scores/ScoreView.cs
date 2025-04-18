using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore;

    public void UpdateView(int score)
    {
        _textScore.text = score.ToString();
    }
}
