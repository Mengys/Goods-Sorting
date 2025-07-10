using _Project.Code.Data.Static.Booster;
using UnityEngine;

namespace _Project.Code.UI.Windows.Base
{
    public class Window : MonoBehaviour, IWindow
    {
        public virtual void Initialize()
        {
        }

        public virtual void OnDestroy()
        {
        }
    }

    public class LoseWindow : Window
    {
        
    }
}