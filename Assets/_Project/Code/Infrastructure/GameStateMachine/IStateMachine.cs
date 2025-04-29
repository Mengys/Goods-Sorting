namespace _Project.Code.Infrastructure.GameStateMachine
{
    public interface IStateMachine<TState> 
    {
        void Enter(TState state);
    }
}