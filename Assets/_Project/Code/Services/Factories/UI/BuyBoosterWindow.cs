using System;
using System.Collections.Generic;
using _Project.Code.Data.Static.Booster;
using _Project.Code.Services.ConfigProvider;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.UI.Elements.Booster.Inventory;
using _Project.Code.UI.Windows.Base;
using _Project.Code.UI.Windows.Implementations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Code.Services.Factories.UI
{
    public interface IAdShower
    {
        void ShowRewarded(Action onFinished = null, Action onFailed = null);
    }

    public class AdShower : IAdShower
    {
        public async void ShowRewarded(Action onFinished = null, Action onFailed = null)
        {
            Debug.Log("Show rewarded ad");
            await System.Threading.Tasks.Task.Delay(2000);
            onFinished?.Invoke();
        }
    }
    
    public class BuyBoosterWindow : Window
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _price;
        
        [SerializeField] private Button _buyForFree;
        [SerializeField] private Button _buyForCoins;
        
        private BoosterId _id;
        private IConfigProvider _configProvider;
        private IProgressProvider _progressProvider;
        private IAdShower _adShower;

        [Inject]
        public void Construct(
            IConfigProvider configProvider,
            IProgressProvider progressProvider,
            IAdShower adShower)
        {
            _adShower = adShower;
            _progressProvider = progressProvider;
            _configProvider = configProvider;
        }

        public void Initialize(BoosterId id, IBoosterInventory inventory)
        {
            var config = _configProvider.ForBooster(id);

            if (!config.HasValue)
                throw new KeyNotFoundException(id.ToString());

            var boosterPrice = config.Value.Asset.Price;
            _price.text = boosterPrice.ToString();
            _icon.sprite = config.Value.Asset.Icon;
            
            _buyForFree.onClick.AddListener(() =>
            {
                _adShower.ShowRewarded(() =>
                {
                    inventory.Add(id);
                    Destroy(gameObject);
                });
            });
            
            _buyForCoins.onClick.AddListener(() =>
            {
                if (_progressProvider.PlayerProgress.Coins >= boosterPrice)
                {
                    _progressProvider.PlayerProgress.Coins -= boosterPrice;
                    inventory.Add(id);
                    Destroy(gameObject);
                }
            });
        }
        
        public override void OnDestroy()
        {
            _buyForFree.onClick.RemoveAllListeners();
            _buyForCoins.onClick.RemoveAllListeners();
        }
    }
}