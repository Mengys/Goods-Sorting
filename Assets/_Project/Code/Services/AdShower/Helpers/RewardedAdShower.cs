using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace _Project.Code.Services.AdShower.Helpers
{
    public class RewardedAdShower : IDisposable
    {
        private RewardedAd _rewardedAd;
        private readonly string _adUnitId;
        private Action _onFinished;
        private Action _onFailed;
        private bool _isLoading;
        private bool _shouldShowAfterLoad;

        public RewardedAdShower(string adUnitId)
        {
            _adUnitId = adUnitId;
            LoadAd();
        }

        private void LoadAd()
        {
            if (_isLoading) return;

            _isLoading = true;
            Debug.Log("Loading rewarded ad...");
            var adRequest = new AdRequest();
            RewardedAd.Load(_adUnitId, adRequest, OnAdLoaded);
        }

        private void OnAdLoaded(RewardedAd ad, LoadAdError error)
        {
            _isLoading = false;

            if (error != null || ad == null)
            {
                Debug.LogError($"Failed to load rewarded ad: {error}");
                _onFailed?.Invoke(); // Если был Show — отреагировать
                return;
            }

            if (_rewardedAd != null)
                UnregisterEventHandlers();

            _rewardedAd = ad;
            RegisterEventHandlers();

            Debug.Log("Rewarded ad loaded successfully.");

            if (_shouldShowAfterLoad)
            {
                _shouldShowAfterLoad = false;
                Show(_onFinished, _onFailed); // Попробовать снова показать
            }
        }

        private void RegisterEventHandlers()
        {
            _rewardedAd.OnAdFullScreenContentClosed += HandleAdClosed;
            _rewardedAd.OnAdFullScreenContentFailed += HandleAdFailedToShow;
        }

        private void UnregisterEventHandlers()
        {
            if (_rewardedAd == null) return;

            _rewardedAd.OnAdFullScreenContentClosed -= HandleAdClosed;
            _rewardedAd.OnAdFullScreenContentFailed -= HandleAdFailedToShow;
        }

        public void Show(Action onFinished = null, Action onFailed = null)
        {
            _onFinished = onFinished;
            _onFailed = onFailed;

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                Debug.Log("Showing rewarded ad.");
                _rewardedAd.Show(HandleUserEarnedReward);
            }
            else
            {
                Debug.LogWarning("Rewarded ad not ready. Will show after load.");
                _shouldShowAfterLoad = true;
                LoadAd();
            }
        }

        private void HandleUserEarnedReward(Reward reward)
        {
            Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");
            Time.timeScale = 1;
            _onFinished?.Invoke();
        }

        private void HandleAdClosed()
        {
            Debug.Log("Rewarded ad closed.");
            LoadAd(); // Загружаем следующую рекламу
        }

        private void HandleAdFailedToShow(AdError error)
        {
            Debug.LogError($"Rewarded ad failed to show: {error}");
            _onFailed?.Invoke();
            LoadAd(); // Перезагружаем при ошибке
        }

        public void Dispose()
        {
            _onFinished = null;
            _onFailed = null;
            UnregisterEventHandlers();
            _rewardedAd = null;
        }
    }
}