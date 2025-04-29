using _Project.Code.Services.UIFactory.WindowOpener;
using _Project.Code.UI.Window;
using UnityEngine;
using Zenject;

namespace _Project.Code.UI.Buttons
{
    public class OpenWindowButton : ButtonClickHandler
    {
        [SerializeField] private WindowId _windowId;
        [Inject] private IWindowOpener _windowOpener;

        protected override void OnClicked() => _windowOpener.Open(_windowId);
    }
}