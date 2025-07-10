using System.IO;
using _Project.Code.Data.Static.Paths;
using UnityEditor;
using UnityEngine;

namespace _Project.Code.Editor
{
    public static class ClearProgressTool
    {
        private const string FileName = DataPaths.PlayerProgress;

        [MenuItem("Tools/Clear " + nameof(DataPaths.PlayerProgress))]
        public static void ClearProgress()
        {
            string path = Path.Combine(Application.persistentDataPath, FileName);

            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"[Tools] Player progress cleared: {path}");
            }
            else
            {
                Debug.Log($"[Tools] No player progress files found: {path}");
            }
        }
    }
}