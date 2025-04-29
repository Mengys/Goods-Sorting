namespace _Project.Code.Architecture.Services.GameStateMachine
{
    public interface IStateMachine<TState> 
    {
        void Enter(TState state);
    }
}