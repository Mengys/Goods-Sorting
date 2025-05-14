using _Project.Code.Services.ProgressProvider;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class CoinsCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IProgressProvider _progressProvider;

        [Inject]
        public void Construct(IProgressProvider progressProvider)
        {
            _progressProvider = progressProvider;
        }
        
        private void Start() =>
            Set(_progressProvider.PlayerProgress.Coins);

        private void Set(int value) =>
            _text.text = value.ToString();
    }
}