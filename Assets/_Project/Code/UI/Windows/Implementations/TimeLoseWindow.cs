using _Project.Code.Gameplay.LevelFlow;
using _Project.Code.Services.StateMachine.Game;
using _Project.Code.UI.Windows.Base;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.UI.Windows.Implementations
{
    public class TimeLoseWindow : Window
    {
        [SerializeField] private TMP_Text _time;
        [SerializeField] private TMP_Text _price;

        [SerializeField] private Button _free;
        [SerializeField] private Button _buy;

        private ILevelFlow _levelFlow;

        private readonly CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(ILevelFlow levelFlow)
        {
            _levelFlow = levelFlow;
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
            _levelFlow.ContinueWithAdditionalTime(30f);
            Destroy(gameObject);
        }
    }
}