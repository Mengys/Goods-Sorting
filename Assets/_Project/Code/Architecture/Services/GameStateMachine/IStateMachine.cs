namespace _Project.Code
{
    public interface IStateMachine<TState> 
    {
        void Enter(TState state);
    }
}