using _Project.Code.Gameplay.Grid.Config;
using _Project.Code.Gameplay.Levels.Configs;
using UnityEditor;
using UnityEngine;

namespace _Project.Code.Gameplay.Grid.CustomEditor.Editor.Shevles
{
    [CustomPropertyDrawer(typeof(GridConfigAsset))]
    public class GridConfigDrawer : PropertyDrawer
    {
        private static readonly PreviewShower previewShower = new PreviewShower();

        static GridConfigDrawer()
        {
            previewShower.Activated += RequestRepaint;
            previewShower.Deactivated += RequestRepaint;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var shelvesProp = property.FindPropertyRelative("Shelves");
            return EditorGUI.GetPropertyHeight(shelvesProp, true) + 24;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var shelvesProp = property.FindPropertyRelative("Shelves");
            Rect shelvesRect = new Rect(position.x, position.y,
                position.width,
                EditorGUI.GetPropertyHeight(shelvesProp, true));
            EditorGUI.PropertyField(shelvesRect, shelvesProp, new GUIContent("Shelves"), true);

            var hostAsset = property.serializedObject.targetObject;
            var target = hostAsset as LevelConfigAsset;
            var shelves = target?.Grid.Shelves;

            if (previewShower.IsActive && shelves != null && !ReferenceEquals(previewShower.CurrentConfigs, shelves))
            {
                previewShower.Hide();
                previewShower.Show(shelves, hostAsset);
            }

            Rect btnRect = new Rect(position.x,
                shelvesRect.yMax + 2,
                position.width, 20);
            string buttonLabel = previewShower.IsActive ? "Hide Preview" : "Preview Grid";

            if (GUI.Button(btnRect, buttonLabel))
            {
                if (!previewShower.IsActive)
                {
                    if (shelves != null)
                        previewShower.Show(shelves, hostAsset);
                    else
                        Debug.LogWarning("No shelves to preview.");
                }
                else if (shelves != null && !ReferenceEquals(previewShower.CurrentConfigs, shelves))
                {
                    previewShower.Hide();
                    previewShower.Show(shelves, hostAsset);
                }
                else
                {
                    previewShower.Hide();
                }
            }

            if (previewShower.IsActive && shelves != null)
            {
                previewShower.UpdateShelves(shelves);
                EditorUtility.SetDirty(hostAsset);
            }

            EditorGUI.EndProperty();
        }

        private static void RequestRepaint()
        {
            foreach (var win in Resources.FindObjectsOfTypeAll<EditorWindow>())
            {
                if (win.titleContent.text == "Inspector")
                    win.Repaint();
            }
        }
    }
}
