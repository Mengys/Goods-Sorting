using UnityEngine;

namespace _Project.Code.UI.Buttons.Window
{
    public class CloseWindowButton : ButtonClickHandler
    {
        [SerializeField] private Windows.Base.Window _window;
        
        protected override void OnClicked()
        {
            _window.OnDestroy();
            Destroy(_window.gameObject);
        }
    }
}