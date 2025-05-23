using _Project.Code.UI.Buttons.Window;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Elements
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Image _progressImage;

        private IComboHandler _comboHandler;

        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        public void Construct(IComboHandler comboHandler)
        {
            _comboHandler = comboHandler;
        }

        private void OnEnable()
        {
            _comboHandler.Level
                .Subscribe(v => _levelText.text = "combo x" + v)
                .AddTo(_disposable);

            _comboHandler.LevelProgress
                .Subscribe(v => _progressImage.fillAmount = v)
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}