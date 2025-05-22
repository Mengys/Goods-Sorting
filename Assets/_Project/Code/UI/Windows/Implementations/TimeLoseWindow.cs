using _Project.Code.Gameplay.LevelFlow;
using _Project.Code.Services.Factories.UI;
using _Project.Code.Services.ProgressProvider;
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
        private IProgressProvider _progressProvider;
        private IAdShower _adShower;
        
        private readonly CompositeDisposable _disposable = new();

        [Inject]
        public void Construct(
            ILevelFlow levelFlow,
            IProgressProvider progressProvider,
            IAdShower adShower)
        {
            _levelFlow = levelFlow;
            _progressProvider = progressProvider;
            _adShower = adShower;
        }

        public override void Initialize()
        {
            _time.text = $"+{GetRewardedTime()}s";
            _price.text = GetPrice().ToString();

            _free.OnClickAsObservable()
                .Subscribe(_ => 
                    _adShower.ShowRewarded(() => Continue()))
                .AddTo(_disposable);

            _buy.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var balance = _progressProvider.PlayerProgress.Coins;
                    var price = GetPrice();

                    if (balance >= price)
                    {
                        _progressProvider.PlayerProgress.Coins -= price;
                        Continue();
                    }
                })
                .AddTo(_disposable);
        }

        private int GetRewardedTime() => 30;
        private int GetPrice() => 100;

        private void Continue()
        {
            _levelFlow.ContinueWithAdditionalTime(30f);
            Destroy(gameObject);
        }

        public override void OnDestroy() =>
            _disposable.Dispose();
    }
}