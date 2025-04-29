namespace _Project.Code.Infrastructure.Services.GameStateMachine
{
    public interface IStateMachine<TState> 
    {
        void Enter(TState state);
    }
}