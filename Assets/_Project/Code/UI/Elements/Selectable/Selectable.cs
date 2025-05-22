using UnityEngine;

namespace _Project.Code.UI.Elements
{
    public abstract class Selectable : MonoBehaviour
    {
        public abstract void Select();
        public abstract void Deselect();
    }
}