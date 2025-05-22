using System;

namespace _Project.Code.Services.Factories.UI.AdShower
{
    public class AdShower : IAdShower, IDisposable
    {
        private readonly RewardedAdController _rewardedController;
        private readonly BannerAdController _bannerController;

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

            _rewardedController = new RewardedAdController(_rewardedAdUnitId);
            _bannerController = new BannerAdController(_bannerAdUnitId);
        }

        public void ShowRewarded(Action onFinished = null, Action onFailed = null)
        {
            _rewardedController.Show(onFinished, onFailed);
        }

        public void ShowBanner()
        {
            _bannerController.Show();
        }

        public void HideBanner()
        {
            _bannerController.Hide();
        }

        public void Dispose()
        {
            _rewardedController.Dispose();
            _bannerController.Dispose();
        }
    }
}