using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Code.Gameplay.Wallet.Infrastructure.Bootstrappers
{
    public class CoinsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        [Inject] private PlayerProgress _playerProgress;
        
        private void Start() =>
            Set(_playerProgress.Coins);

        private void Set(int value) =>
            _text.text = value.ToString();
    }
}