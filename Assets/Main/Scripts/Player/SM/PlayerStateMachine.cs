using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.StateMachine.GameSM;
using UnityEngine;

namespace Main.Scripts.Player.SM
{
    public class PlayerStateMachine : MonoBehaviour, IMovable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float _speed;
        
        private IInputService _inputService;
        private BaseStateMachine _stateMachine;
        
        public void Init(IInputService inputService)
        {
            _inputService = inputService;
            _stateMachine = new BaseStateMachine();
            SetupStateMachine();
        }

        public void Move(Vector2 moveDir)
        {
            transform.position += (Vector3)moveDir * _speed * Time.deltaTime;
        }

        private void Update()
        {
            _stateMachine?.UpdateStateLogic();
        }

        private void FixedUpdate()
        {
            _stateMachine?.UpdateStatePhysics();
        }

        private void SetupStateMachine()
        {
            _stateMachine.AddState(typeof(IdleState), new IdleState(_stateMachine, _inputService));
            _stateMachine.AddState(typeof(MovementState), new MovementState(_stateMachine, _inputService, this, _animator));
            _stateMachine.Enter<IdleState>();
        }
    }
}
