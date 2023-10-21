using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.StateMachine;

namespace Main.Scripts.Player.SM
{
    public class IdleState : IState, IUpdatableState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IInputService _inputService;

        public IdleState(IStateMachine stateMachine, IInputService inputService)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
        }
        
        public void UpdateLogic()
        {
            _inputService.GetMovementAxis(out bool isPressing);
            if (isPressing) {
                _stateMachine.Enter<MovementState>();
            }
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            
        }
    }
}