using System.Threading.Tasks;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.ApplicationLifecycle;
using _Project.Code.Services.DataPersistence;
using _Project.Code.Services.PauseHandler;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.StateMachine;
using R3;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class EntryBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        [Inject] private IProgressProvider _progressProvider;
        [Inject] private IDataPersistence<PlayerProgress> _dataPersistence;
        [Inject] private IPauseHandler _pauseHandler;
        [Inject] private AppLifeCycleEvents _appLifeCycleEvents;

        public override void InstallBindings()
        {
        }

        private async void Awake()
        {
            _progressProvider.PlayerProgress.Value = 
                await _dataPersistence.LoadAsync() ?? new PlayerProgress();

            _stateMachine.Enter(GameStateId.Menu);
        }
    }
}