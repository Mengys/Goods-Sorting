using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Scores
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textScore;

        public void UpdateView(int score)
        {
            _textScore.text = score.ToString();
        }
    }
}
