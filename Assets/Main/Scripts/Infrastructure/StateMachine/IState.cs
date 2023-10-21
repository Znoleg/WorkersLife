namespace Main.Scripts.Infrastructure.StateMachine
{
    public interface IPhysicsUpdatableState
    {
        void UpdatePhysics();
    }
    
    public interface IUpdatableState
    {
        void UpdateLogic();
    }

    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }

    public interface IExitableState
    {
        void Exit();
    }
}