using System;
using _Project.Code.Services.ApplicationLifecycle;
using _Project.Code.Services.PauseHandler;
using R3;

namespace _Project.Code.Utils
{
    public static class PauseHandlerExtensions
    {
        public static IDisposable HandleAppLifeCycleEvents(this IPauseHandler pauseHandler,
            AppLifeCycleEvents events)
        {
            var disposable = new CompositeDisposable();

            events.Paused.Subscribe(_ => pauseHandler.Pause()).AddTo(disposable);
            events.Focused.Subscribe(_ => pauseHandler.Resume()).AddTo(disposable);

            return disposable;
        }
    }
}