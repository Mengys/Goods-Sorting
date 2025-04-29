using UnityEngine;

namespace _Project.Code.UI.Window
{
    public class WindowBase : MonoBehaviour, IWindow
    {
        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}