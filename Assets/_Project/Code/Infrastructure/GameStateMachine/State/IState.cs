namespace _Project.Code.Infrastructure.GameStateMachine.State
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}