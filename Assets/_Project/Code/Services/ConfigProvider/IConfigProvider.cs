using _Project.Code.Infrastructure.GameStateMachine.Config;
using _Project.Code.Infrastructure.GameStateMachine.State;
using _Project.Code.Services.UIFactory.Config;
using _Project.Code.UI.Window;

namespace _Project.Code.Services.ConfigProvider
{
    public interface IConfigProvider
    {
        WindowConfig? ForWindow(WindowId id);
        GameStateConfig? ForState(GameStateId stateId);
    }
}