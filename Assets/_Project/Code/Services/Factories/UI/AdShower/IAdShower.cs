using System;

namespace _Project.Code.Services.Factories.UI
{
    public interface IAdShower
    {
        void ShowRewarded(Action onFinished = null, Action onFailed = null);
        void ShowBanner();
    }
}