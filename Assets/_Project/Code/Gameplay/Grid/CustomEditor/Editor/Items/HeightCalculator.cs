using UnityEditor;

namespace _Project.Code.Gameplay.Grid.CustomEditor.Editor.Items
{
    public static class HeightCalculator
    {
        private const float MarginY = 8f, Spacing = 4f;
        private static float LineH => EditorGUIUtility.singleLineHeight;
        private static float CellSize => LineH * 1.5f;

        public static float Calculate(int rows, bool hasSelection)
        {
            float h = MarginY + LineH + Spacing;
            h += LineH + Spacing * 2;
            h += rows * (CellSize + Spacing);
            if (hasSelection) h += (LineH + Spacing) * 2;
            h += MarginY;
            return h;
        }
    }
}