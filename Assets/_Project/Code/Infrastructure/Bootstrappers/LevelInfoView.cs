using _Project.Code.UI.Windows.Implementations.LevelInfo;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers
{
    public class LevelInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _difficulty;

        [Inject] private PlayerProgress _progress;

        private void Start() =>
            Set(_progress.Level);

        private void Set(LevelInfo value)
        {
            _number.text = "Level " + value.Number;

            if (_difficulty != null)
                _difficulty.text = value.Difficulty + " level";
        }
    }
}