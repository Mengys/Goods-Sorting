using _Project.Code.UI.Windows.Base;

namespace _Project.Code.Services.Factories.UI
{
    public interface IWindowFactory
    {
        Window Create(WindowId id);
    }
}