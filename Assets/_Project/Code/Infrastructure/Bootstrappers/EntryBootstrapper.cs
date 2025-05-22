using System;
using System.Threading.Tasks;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Data.Static.GameState;
using _Project.Code.Services.DataPersistence;
using _Project.Code.Services.ProgressProvider;
using _Project.Code.Services.StateMachine;
using Firebase;
using Firebase.Extensions;
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

        public override void InstallBindings()
        {
        }

        private async void Awake()
        {
            _progressProvider.PlayerProgress.Value = 
                await _dataPersistence.LoadAsync() ?? ProgressProvider.DefaultPlayerProgress;
            
            await FirebaseApp.CheckDependenciesAsync()
                .ContinueWithOnMainThread(OnDependencyStatusReceived);
            
            MobileAds.Initialize(OnMobileAdsInitialized);
        }

        private void OnMobileAdsInitialized(InitializationStatus initializationStatus)
        {
            Debug.Log("Mobile ads initialized");
            _stateMachine.Enter(GameStateId.Menu);
        }

        private void OnDependencyStatusReceived(Task<DependencyStatus> task)
        {
            try
            {
                if (!task.IsCompletedSuccessfully)
                    throw new Exception("Firebase dependency check failed", task.Exception);
                
                var status = task.Result;

                if (status != DependencyStatus.Available)
                    throw new Exception("Firebase dependency check failed: " + status);
                
                Debug.Log("Firebase initialized successfully!");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}