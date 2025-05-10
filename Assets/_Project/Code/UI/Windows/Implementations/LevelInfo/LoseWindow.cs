using _Project.Code.Infrastructure.GameStateMachine;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.Factories.Level;
using _Project.Code.Services.SceneArgs;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations.LevelInfo
{
    public class LoseWindow : Window
    {
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _price;

        [SerializeField] private Button _free;
        [SerializeField] private Button _buy;
        
        private Level _level;
        
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(Level level)
        {
            _level = level;
        }

        public override void Initialize()
        {
            _time.text = "+30s";
            _price.text = "100";

            _free.OnClickAsObservable()
                .Subscribe(_ => Continue())
                .AddTo(_disposable);

            _buy.OnClickAsObservable()
                .Subscribe(_ => Continue())
                .AddTo(_disposable);
        }

        public override void OnDestroy() =>
            _disposable.Dispose();

        private void Continue()
        {
            _level.ContinueWithAdditionalTime(30f);
            Destroy(gameObject);
        }
    }
}