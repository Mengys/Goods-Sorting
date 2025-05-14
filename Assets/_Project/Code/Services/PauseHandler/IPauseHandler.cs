using System;

namespace _Project.Code.Services.PauseHandler
{
    public interface IPauseHandler
    {
        public bool IsPaused { get; }
        public void Pause();
        public void Resume();
        public IDisposable Register(IPausable pausable);
    }
}