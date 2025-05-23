using System;
using _Project.Code.Data.Dynamic.PlayerProgress;
using _Project.Code.Services.ApplicationLifecycle;
using _Project.Code.Services.DataPersistence;
using _Project.Code.Services.ProgressProvider;
using R3;
using UnityEngine;

namespace _Project.Code.Utils
{
    public static class DataPersistenceExtensions
    {
        public static IDisposable HandleAppQuit(
            this IDataPersistence<PlayerProgress> dataPersistence,
            IProgressProvider progressProvider,
            AppLifeCycleEvents appLifeCycleEvents)
        {
            return appLifeCycleEvents.Quit
                .SubscribeAwait(
                    async (_, _) =>
                    {
                        var data = progressProvider.PlayerProgress.Serializable;
                        
                        try
                        {
                            await dataPersistence.SaveAsync(data).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"[DataPersistence] Failed to save on quit: {ex}");
                        }
                    },
                    awaitOperation: AwaitOperation.Sequential,
                    configureAwait: true
                );
        }
    }
}