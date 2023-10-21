using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.StateMachine;
using UnityEngine;

namespace Main.Scripts.Player.SM
{
    public class MovementState : IState, IUpdatableState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IInputService _inputService;
        private readonly Animator _animatorController;
        private readonly IMovable _movable;

        private static readonly int MovingLeftHash = Animator.StringToHash("MovingLeft");
        private static readonly int MovingRightHash = Animator.StringToHash("MovingRight");
        private static readonly int MovingUpHash = Animator.StringToHash("MovingUp");
        private static readonly int MovingDownHash = Animator.StringToHash("MovingDown");

        public MovementState(IStateMachine stateMachine, IInputService inputService, IMovable movable, Animator animatorController)
        {
            _stateMachine = stateMachine;
            _inputService = inputService;
            _movable = movable;
            _animatorController = animatorController;
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            
        }

        public void UpdateLogic()
        {
            Vector2 axis = _inputService.GetMovementAxis(out bool isPressing);
            if (!isPressing)
            {
                _stateMachine.Enter<IdleState>();
                return;
            }
            
            Vector2 modifiedAxis = axis;

            if (axis.x != 0f)
            {
                modifiedAxis.y = 0f;
                _animatorController.Play(axis.x > 0f ? MovingRightHash : MovingLeftHash);
            }
            else if (axis.y != 0f)
            {
                modifiedAxis.x = 0f;
                _animatorController.Play(axis.y > 0f ? MovingUpHash : MovingDownHash);
            }

            _movable.Move(modifiedAxis);
        }
    }
}