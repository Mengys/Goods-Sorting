using _Project.Code.Data.Dynamic;
using _Project.Code.Services.ProgressProvider;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class PlayLevelButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _difficulty;

        private IProgressProvider _provider;

        [Inject]
        public void Construct(IProgressProvider progressProvider) =>
            _provider = progressProvider;
        
        private void Start() =>
            Set(_provider.PlayerProgress.Level);

        private void Set(LevelInfo value)
        {
            _number.text = "Level " + value.Number;

            if (_difficulty != null)
                _difficulty.text = value.Difficulty + " level";
        }
    }
}