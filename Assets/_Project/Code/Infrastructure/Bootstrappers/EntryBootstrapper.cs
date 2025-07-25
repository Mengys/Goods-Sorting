using System;
using System.Threading.Tasks;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.DataPersistence;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.StateMachine;
#if !UNITY_WEBGL
using Firebase;
#endif
using GoogleMobileAds.Api;
using R3;
using UnityEngine;
using Zenject;

namespace _Project.Code.Infrastructure.Bootstrappers
{
    public class EntryBootstrapper : MonoInstaller
    {
        [Inject] private IStateMachine<GameStateId> _stateMachine;
        [Inject] private IProgressProvider _progressProvider;
        [Inject] private IDataPersistence<PlayerProgress> _dataPersistence;

        public override void InstallBindings() { }

        private async void Awake()
        {
            await InitializeFirebaseAsync();
            await InitializeMobileAdsAsync();
            await InitializeProgressAsync();

            _stateMachine.Enter(GameStateId.Gameplay);
        }

        private async Task InitializeProgressAsync()
        {
            var progress = await _dataPersistence.LoadAsync();
            _progressProvider.PlayerProgress.Value = progress ?? ProgressProvider.DefaultPlayerProgress;
        }

        private async Task InitializeFirebaseAsync()
        {
#if !UNITY_WEBGL
            try
            {
                var status = await FirebaseApp.CheckDependenciesAsync();
                if (status != DependencyStatus.Available)
                    throw new InvalidOperationException($"Firebase dependency check failed: {status}");

                Debug.Log("Firebase initialized successfully!");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
#endif
        }

        private async Task InitializeMobileAdsAsync()
        {
            try
            {
                await MobileAdsInitializeAsync();
                Debug.Log("Mobile ads initialized");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private Task MobileAdsInitializeAsync()
        {
            var tcs = new TaskCompletionSource<InitializationStatus>();

            MobileAds.Initialize(initStatus =>
            {
                if (initStatus != null)
                    tcs.SetResult(initStatus);
                else
                    tcs.SetException(new Exception("MobileAds initialization failed: status is null"));
            });

            return tcs.Task;
        }
    }
}
