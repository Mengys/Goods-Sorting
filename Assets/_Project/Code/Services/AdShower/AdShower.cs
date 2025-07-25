using System;
using System.Diagnostics;
using _Project.Code.Services.AdShower.Helpers;
using UnityEngine.Scripting;

namespace _Project.Code.Services.AdShower
{
    [Preserve]
    public class AdShower : IAdShower, IDisposable
    {
        private readonly RewardedAdShower _rewardedShower;
        private readonly BannerAdShower _bannerShower;

        private readonly string _rewardedAdUnitId;
        private readonly string _bannerAdUnitId;

        public AdShower()
        {
#if UNITY_ANDROID
            _rewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";
            _bannerAdUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
            _rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
            _bannerAdUnitId = "ca-app-pub-3940256099942544/2934735716";

            _rewardedAdUnitId = "unused";
            _bannerAdUnitId = "unused";

            Console.WriteLine("test");

            _rewardedShower = new RewardedAdShower(_rewardedAdUnitId);
            _bannerShower = new BannerAdShower(_bannerAdUnitId);
#endif
        }

        public void ShowRewarded(Action onFinished = null, Action onFailed = null)
        {
#if !UNITY_WEBGL
            _rewardedShower.Show(onFinished, onFailed);
#else 
            YandexAdv.Instance.ShowRewardAdv();
            onFinished?.Invoke();
#endif
        }

        public void ShowBanner()
        {
#if !UNITY_WEBGL
            _bannerShower.Show();
#endif
        }

        public void HideBanner()
        {
#if !UNITY_WEBGL
            _bannerShower.Hide();
#endif
        }

        public void Dispose()
        {
#if !UNITY_WEBGL
            _rewardedShower.Dispose();
            _bannerShower.Dispose();
#endif
        }
    }
}