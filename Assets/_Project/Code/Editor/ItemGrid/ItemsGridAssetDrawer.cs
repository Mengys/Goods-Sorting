using _Project.Code.Data.Static.Grid;
using UnityEditor;
using UnityEngine;

namespace _Project.Code.Editor.ItemGrid
{
    [CustomPropertyDrawer(typeof(ItemGridAssetConfig))]
    public class ItemGridAssetDrawer : PropertyDrawer
    {
        private readonly SelectionState _state = new SelectionState();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //property.serializedObject.Update();

            var context = new ItemGridContext(property, position, label, _state);

            EditorGUI.BeginProperty(position, label, property);

            context.DrawLabel();
            context.DrawRowControls();
            context.DrawGrid();
            context.DrawCellControls();

            context.ApplySelection();

            EditorGUI.EndProperty();
            //property.serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var rows = Mathf.Max(1, property.FindPropertyRelative("Rows").intValue);
            return HeightCalculator.Calculate(rows, _state.HasSelection(property.propertyPath));
        }
    }
}