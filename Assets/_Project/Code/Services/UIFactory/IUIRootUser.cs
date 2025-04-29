using _Project.Code.UI.UIRoot;

namespace _Project.Code.Services.UIFactory
{
    public interface IUIRootUser
    {
        void Initialize(IUIRoot uiRoot);
        void Cleanup();
    }
}