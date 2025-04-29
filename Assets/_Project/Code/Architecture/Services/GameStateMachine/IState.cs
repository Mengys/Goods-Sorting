namespace _Project.Code.Architecture.Services.GameStateMachine
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}