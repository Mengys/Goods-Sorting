using System.Collections.Generic;

namespace _Project.Code.Editor.ItemGrid
{
    internal class SelectionState
    {
        public readonly Dictionary<string, int> Selected = new Dictionary<string, int>();
        public readonly Dictionary<string, bool> Editing = new Dictionary<string, bool>();

        public void TryInit(string key)
        {
            if (!Selected.ContainsKey(key)) Selected[key] = -1;
            if (!Editing.ContainsKey(key)) Editing[key] = false;
        }

        public bool HasSelection(string key)
        {
            if (Selected.TryGetValue(key, out int idx))
                return idx >= 0;
            return false;
        }
    }
}