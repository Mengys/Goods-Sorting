using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Code.Data.Services;
using _Project.Code.Data.Static.Grid;
using _Project.Code.Data.Static.Shelf;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Services.ConfigProvider;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Code.Editor.ShelfGrid
{
    public class PreviewShower
    {
        public event Action Activated;
        public event Action Deactivated;

        public bool IsActive { get; private set; }
        public IReadOnlyList<ShelfConfigAsset> CurrentConfigs => _shelfConfigs;

        private Object _hostAsset;
        private readonly List<ShelfView> _spawnedShelves = new();
        private List<ShelfConfigAsset> _shelfConfigs;

        public void Show(List<ShelfConfigAsset> shelfConfigs, Object hostAsset)
        {
            DestroyPreviews();
            if (IsActive)
                Hide();

            IsActive = true;
            _shelfConfigs = shelfConfigs;
            _hostAsset = hostAsset;

            var configProvider = ConfigProvider.NewInstance;
            var gridFactory = new PreviewGridFactory(configProvider);
            var gridConfigAsset = new GridConfigAsset { Shelves = shelfConfigs };
            var gridConfig = AssetDataFormatter.AsGridConfig(gridConfigAsset);

            var parent = Object.FindObjectOfType<Canvas>()?.transform;
            var instances = gridFactory.Create(gridConfig, parent);
            _spawnedShelves.AddRange(instances);

            foreach (var (view, i) in _spawnedShelves.Select((v, idx) => (v, idx)))
            {
                var go = view.gameObject;
                go.name = $"ShelfPreview_{i}";
                go.AddComponent<ShelfPreview>();
                // Prevent these preview objects from being saved into the scene
                go.hideFlags |= HideFlags.DontSave;
            }

            SceneView.duringSceneGui += OnSceneGUI;
            Activated?.Invoke();
            RepaintInspector();
        }

        public void Hide()
        {
            if (!IsActive) return;
            IsActive = false;

            SceneView.duringSceneGui -= OnSceneGUI;
            foreach (var obj in _spawnedShelves)
                if (obj != null)
                    Object.DestroyImmediate(obj.gameObject);

            DestroyPreviews();
            _spawnedShelves.Clear();
            _shelfConfigs = null;
            _hostAsset = null;

            Deactivated?.Invoke();
            RepaintInspector();
        }

        public void UpdateShelves(List<ShelfConfigAsset> shelves)
        {
            if (!IsActive || shelves.Count != _spawnedShelves.Count) return;
            for (int i = 0; i < shelves.Count; i++)
            {
                if (_spawnedShelves[i] == null) continue;
                _spawnedShelves[i].Position = shelves[i].Position;
            }
        }

        private static void DestroyPreviews()
        {
            var previews = Resources.FindObjectsOfTypeAll<ShelfPreview>();
            
            foreach (var preview in previews)
                Object.DestroyImmediate(preview.gameObject);
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (!IsActive || _shelfConfigs == null || _hostAsset == null) return;

            for (int i = 0; i < _spawnedShelves.Count; i++)
            {
                var view = _spawnedShelves[i];
                if (view == null) continue;

                EditorGUI.BeginChangeCheck();
                Vector3 newPos = Handles.PositionHandle(view.transform.position, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(view.transform, "Move Preview Shelf");
                    Undo.RecordObject(_hostAsset, "Move Shelf Config");

                    view.transform.position = newPos;
                
                    var old = _shelfConfigs[i];
                    _shelfConfigs[i] = new ShelfConfigAsset
                    {
                        Id = old.Id,
                        Position = view.Position,
                        ItemGridAssetConfig = old.ItemGridAssetConfig
                    };
                
                    EditorUtility.SetDirty(_hostAsset);
                    // Don't mark scene dirty for preview-only objects
                }
            }
        }

        private void RepaintInspector()
        {
            foreach (var win in Resources.FindObjectsOfTypeAll<EditorWindow>())
                if (win.titleContent.text == "Inspector")
                    win.Repaint();
        }
    }
}
