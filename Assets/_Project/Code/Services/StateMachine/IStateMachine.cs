namespace _Project.Code.Services.StateMachine
{
    public interface IStateMachine<TState> 
    {
        void Enter(TState state);
    }
}