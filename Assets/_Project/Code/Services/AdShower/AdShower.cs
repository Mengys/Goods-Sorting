using System;
using _Project.Code.Services.AdShower.Helpers;

namespace _Project.Code.Services.AdShower
{
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
#else
            _rewardedAdUnitId = "unused";
            _bannerAdUnitId = "unused";
#endif

            _rewardedShower = new RewardedAdShower(_rewardedAdUnitId);
            _bannerShower = new BannerAdShower(_bannerAdUnitId);
        }

        public void ShowRewarded(Action onFinished = null, Action onFailed = null)
        {
            _rewardedShower.Show(onFinished, onFailed);
        }

        public void ShowBanner()
        {
            _bannerShower.Show();
        }

        public void HideBanner()
        {
            _bannerShower.Hide();
        }

        public void Dispose()
        {
            _rewardedShower.Dispose();
            _bannerShower.Dispose();
        }
    }
}