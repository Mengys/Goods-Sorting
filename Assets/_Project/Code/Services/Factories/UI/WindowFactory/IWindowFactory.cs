using _Project.Code.UI.Windows;

namespace _Project.Code.Services.Factories.UI.WindowFactory
{
    public interface IWindowFactory
    {
        Window Create(WindowId id);
    }
}