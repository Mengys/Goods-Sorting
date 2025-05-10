using _Project.Code.Services.Factories.UI.WindowFactory;
using _Project.Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Buttons
{
    public class OpenWindowButton : ButtonClickHandler
    {
        [SerializeField] private WindowId _windowId;
        
        [Inject] private IWindowFactory _windowFactory;

        protected override void OnClicked() => 
            _windowFactory.Create(_windowId);
    }
}