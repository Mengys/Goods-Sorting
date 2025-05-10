using _Project.Code.UI.Windows;
using UnityEngine;

namespace _Project.Code.UI.Buttons
{
    public class CloseWindowButton : ButtonClickHandler
    {
        [SerializeField] private Window _window;
        
        protected override void OnClicked()
        {
            _window.OnDestroy();
            Destroy(_window.gameObject);
        }
    }
}