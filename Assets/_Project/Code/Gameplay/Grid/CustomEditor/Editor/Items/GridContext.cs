using _Project.Code.Gameplay.Grid.Config;
using _Project.Code.Gameplay.Shelves;
using _Project.Code.Gameplay.Shelves.Configs;
using _Project.Code.Services.ConfigProvider;
using UnityEditor;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.CustomEditor.Editor.Items
{
    internal class GridContext
    {
        private const float MarginX = 0f, MarginY = 8f, Spacing = 4f;
        private static float LineH => EditorGUIUtility.singleLineHeight;
        private static float CellSize => LineH * 1.5f;

        private readonly SerializedProperty _cellsProp;
        private readonly SerializedProperty _rowsProp;
        private readonly SerializedProperty _columnsProp;
    
        private readonly string _shelfId;
        private readonly Rect _baseRect;
        private readonly SelectionState _state;
        private readonly string _key;

        private int _rows;
        private int _selectedIndex;
        private bool _isEditing;
        private float _y;

        public GridContext(SerializedProperty property, Rect pos, GUIContent label, SelectionState state)
        {
            _baseRect = new Rect(pos.x + MarginX, pos.y + MarginY, pos.width - MarginX * 2, pos.height - MarginY * 2);
        
            _cellsProp = property.FindPropertyRelative("Cells");
            _rowsProp = property.FindPropertyRelative("Rows");
            _columnsProp = property.FindPropertyRelative("Columns");
        
            _shelfId = TryGetShelfId(property);
            _state = state;
            _key = property.propertyPath;

            state.TryInit(_key);
            _selectedIndex = state.Selected[_key];
            _isEditing = state.Editing[_key];

            _rows = Mathf.Max(1, _rowsProp.intValue);
            _rowsProp.intValue = _rows;

            var config = ConfigProvider
                .NewInstance
                .ForShelf(new ShelfId(_shelfId));

            _columnsProp.intValue = config != null && config.Value.Prefab != null
                ? config.Value.Prefab.ColumnsCount
                : 0;

            EnsureArraySize();

            // Reset invalid selection
            if (_selectedIndex < 0 || _selectedIndex >= _cellsProp.arraySize)
            {
                _selectedIndex = -1;
                state.Selected[_key] = -1;
                state.Editing[_key] = false;
            }

            _y = _baseRect.y;
        }

        public void DrawLabel()
        {
            EditorGUI.LabelField(new Rect(_baseRect.x, _y, _baseRect.width, LineH), "Items", EditorStyles.boldLabel);
            _y += LineH + Spacing;
        }

        public void DrawRowControls()
        {
            var labelRect = new Rect(_baseRect.x, _y, 80, LineH);
            EditorGUI.LabelField(labelRect, $"Layers: {_rows}");
            float bx = labelRect.xMax + Spacing;
            if (GUI.Button(new Rect(bx, _y, 30, LineH), "-") && _rows > 1)
                ChangeRows(-1);
            if (GUI.Button(new Rect(bx + 35, _y, 30, LineH), "+"))
                ChangeRows(+1);
            _y += LineH + Spacing * 2;
        }

        public void DrawGrid()
        {
            float startX = _baseRect.x + 15;
            float startY = _y;
            for (int r = _rows - 1; r >= 0; r--)
            {
                for (int c = 0; c < _columnsProp.intValue; c++)
                {
                    int idx = r * _columnsProp.intValue + c;
                    if (idx >= _cellsProp.arraySize) continue;

                    var rect = new Rect(startX + c * (CellSize + Spacing),
                        startY + (_rows - 1 - r) * (CellSize + Spacing),
                        CellSize, CellSize);
                    DrawCellButton(idx, rect);
                }
            }

            _y = startY + _rows * (CellSize + Spacing) + Spacing;
        }

        public void DrawCellControls()
        {
            if (_selectedIndex < 0 || _selectedIndex >= _cellsProp.arraySize)
                return; // no valid selection

            var selProp = _cellsProp.GetArrayElementAtIndex(_selectedIndex);
            var textRect = new Rect(_baseRect.x, _y, _baseRect.width, LineH);
            var btnRect = new Rect(_baseRect.x + 15, _y + LineH + Spacing, _baseRect.width - 15, LineH);

            if (string.IsNullOrEmpty(selProp.stringValue) && !_isEditing)
            {
                if (GUI.Button(btnRect, "Add"))
                    _state.Editing[_key] = true;
            }
            else
            {
                if (GUI.Button(btnRect, "Remove"))
                    RemoveSelection(selProp);

                selProp.stringValue = EditorGUI.TextField(textRect, "Item Id: ", selProp.stringValue);
            }
        }

        public void ApplySelection()
        {
            _state.Selected[_key] = _selectedIndex;
        }

        private void DrawCellButton(int idx, Rect rect)
        {
            Color orig = GUI.backgroundColor;
            if (idx == _selectedIndex)
                GUI.backgroundColor = Color.cyan;
            else if (!string.IsNullOrEmpty(_cellsProp.GetArrayElementAtIndex(idx).stringValue))
                GUI.backgroundColor = Color.green;

            if (GUI.Button(rect, GUIContent.none))
            {
                _selectedIndex = idx;
                _state.Editing[_key] = false;

                GUI.FocusControl(null);
                GUIUtility.keyboardControl = 0;
            }

            GUI.backgroundColor = orig;
        }

        private void ChangeRows(int delta)
        {
            _rowsProp.intValue = _rows + delta;
            _rows += delta;
            EnsureArraySize();

            // Reset selection after resizing
            _selectedIndex = -1;
            _state.Selected[_key] = -1;
            _state.Editing[_key] = false;
        }

        private void EnsureArraySize()
        {
            int needed = _rows * _columnsProp.intValue;
            if (_cellsProp.arraySize != needed)
                _cellsProp.arraySize = needed;
        }

        private void RemoveSelection(SerializedProperty selProp)
        {
            selProp.stringValue = null;
            _state.Editing[_key] = false;
        }

        private string TryGetShelfId(SerializedProperty property)
        {
            string path = property.propertyPath.Replace(nameof(ItemGridAssetConfig), nameof(ShelfConfigAsset.Id));
            var idProp = property.serializedObject.FindProperty(path);

            if (idProp != null && idProp.propertyType == SerializedPropertyType.String)
                return idProp.stringValue;

            Debug.LogWarning($"[GridContext] ShelfId property at path '{path}' is not a string or was not found.");
            return "unknown";
        }
    }
}