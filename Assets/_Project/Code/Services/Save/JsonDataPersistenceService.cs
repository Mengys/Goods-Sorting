using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class JsonDataPersistenceService<T> : IDataPersistenceService<T> where T : class, new()
{
    private readonly string _filePath;

    public JsonDataPersistenceService(string fileName)
    {
        _filePath = Path.Combine(Application.persistentDataPath, fileName);
    }

    public async Task SaveAsync(T data)
    {
        try
        {
            string json = JsonUtility.ToJson(data, true);
            // Ensure directory exists
            string dir = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                await writer.WriteAsync(json);
            }

            Debug.Log($"[DataService] Successfully saved data to {_filePath}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[DataService] Failed saving data: {ex}");
        }
    }

    public async Task<T> LoadAsync()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                Debug.LogWarning($"[DataService] No save file found at {_filePath}, returning new instance.");
                return new T();
            }

            using (StreamReader reader = new StreamReader(_filePath))
            {
                string json = await reader.ReadToEndAsync();
                T data = JsonUtility.FromJson<T>(json);
                Debug.Log($"[DataService] Successfully loaded data from {_filePath}");
                return data;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[DataService] Failed loading data: {ex}");
            return new T();
        }
    }
}