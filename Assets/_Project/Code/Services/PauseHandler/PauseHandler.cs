using System;
using System.Collections.Generic;
using R3;

namespace _Project.Code.Services.PauseHandler
{
    public class PauseHandler : IPauseHandler, IDisposable
    {
        private readonly List<IPausable> _pausables = new();

        public bool IsPaused { get; private set; }

        public void Pause()
        {
            _pausables.ForEach(pausable => pausable.Pause());
            IsPaused = true;
        }

        public void Resume()
        {
            _pausables.ForEach(pausable => pausable.Resume());
            IsPaused = false;
        }

        public IDisposable Register(IPausable pausable)
        {
            if (_pausables.Contains(pausable)) 
                return Disposable.Empty;
            
            _pausables.Add(pausable);
            
            return Disposable.Create(() =>
                _pausables.Remove(pausable));
        }

        public void Dispose() => _pausables.Clear();
    }
}