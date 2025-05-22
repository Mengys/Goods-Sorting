using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Code.Services.DataPersistence
{
    public class JsonFileDataPersistence<T> : IDataPersistence<T>
    {
        private readonly string _filePath;

        public JsonFileDataPersistence(string fileName)
        {
            _filePath = Path.Combine(Application.persistentDataPath, fileName);
        }

        public async Task SaveAsync(T data)
        {
            Debug.Log($"[DataPersistence] Saving data to {_filePath}");
            
            var json = JsonUtility.ToJson(data, true);
            await using var writer = new StreamWriter(_filePath, false);
            await writer.WriteAsync(json);
        }

        public async Task<T> LoadAsync()
        {
            Debug.Log($"[DataPersistence] Loading data from {_filePath}");

            if (!File.Exists(_filePath))
                return default;

            using var reader = new StreamReader(_filePath);
            var json = await reader.ReadToEndAsync();
            return JsonUtility.FromJson<T>(json);
        }

        public Task ClearAsync()
        {
            Debug.Log($"[DataPersistence] Clearing data from {Path.GetFileName(_filePath)}");

            if (File.Exists(_filePath))
                File.Delete(_filePath);

            return Task.CompletedTask;
        }
    }
}