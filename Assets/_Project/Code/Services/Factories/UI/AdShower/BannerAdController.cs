using System;
using GoogleMobileAds.Api;
using UnityEngine;

namespace _Project.Code.Services.Factories.UI.AdShower
{
    public class BannerAdController : IDisposable
    {
        private readonly string _adUnitId;
        private BannerView _bannerView;

        public BannerAdController(string adUnitId)
        {
            _adUnitId = adUnitId;
        }

        public void Show()
        {
            if (_bannerView != null)
                return;

            var adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
            _bannerView = new BannerView(_adUnitId, adSize, AdPosition.Bottom);
            _bannerView.LoadAd(new AdRequest());
        }

        public void Hide()
        {
            _bannerView?.Hide();
        }

        public void Dispose()
        {
            _bannerView?.Destroy();
            _bannerView = null;
        }
    }
}