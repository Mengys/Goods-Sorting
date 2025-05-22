using UnityEngine;

namespace _Project.Code.UI.Elements.Booster.Selectable
{
    public abstract class Selectable : MonoBehaviour
    {
        public abstract void Select();
        public abstract void Deselect();
    }
}