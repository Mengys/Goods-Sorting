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

        public RewardedAdShower(string adUnitId)
        {
            _adUnitId = adUnitId;
            LoadAd();
        }

        private void LoadAd()
        {
            var adRequest = new AdRequest();
            RewardedAd.Load(_adUnitId, adRequest, OnAdLoaded);
        }

        private void OnAdLoaded(RewardedAd ad, LoadAdError error)
        {
            if (error != null || ad == null)
            {
                Debug.LogError($"Failed to load rewarded ad: {error}");
                return;
            }

            _rewardedAd = ad;
            RegisterEventHandlers();
        }

        private void RegisterEventHandlers()
        {
            _rewardedAd.OnAdFullScreenContentClosed += HandleAdClosed;
            _rewardedAd.OnAdFullScreenContentFailed += HandleAdFailedToShow;
        }

        public void Show(Action onFinished = null, Action onFailed = null)
        {
            _onFinished = onFinished;
            _onFailed = onFailed;

            if (_rewardedAd != null && _rewardedAd.CanShowAd())
            {
                _rewardedAd.Show(HandleUserEarnedReward);
            }
            else
            {
                Debug.LogWarning("Rewarded ad is not ready to be shown.");
                _onFailed?.Invoke();
                LoadAd(); // Attempt to load a new ad
            }
        }

        private void HandleUserEarnedReward(Reward reward)
        {
            Debug.Log($"User earned reward: {reward.Amount} {reward.Type}");
            _onFinished?.Invoke();
        }

        private void HandleAdClosed()
        {
            Debug.Log("Rewarded ad closed.");
            LoadAd(); // Load a new ad for the next opportunity
        }

        private void HandleAdFailedToShow(AdError error)
        {
            Debug.LogError($"Rewarded ad failed to show: {error}");
            _onFailed?.Invoke();
            LoadAd(); // Attempt to load a new ad
        }

        public void Dispose()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.OnAdFullScreenContentClosed -= HandleAdClosed;
                _rewardedAd.OnAdFullScreenContentFailed -= HandleAdFailedToShow;
                _rewardedAd = null;
            }
        }
    }
}
