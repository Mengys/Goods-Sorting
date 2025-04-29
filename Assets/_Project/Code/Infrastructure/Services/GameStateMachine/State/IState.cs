namespace _Project.Code.Infrastructure.Services.GameStateMachine.State
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}