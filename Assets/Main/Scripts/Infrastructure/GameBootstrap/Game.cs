using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Main.Scripts.Infrastructure.StateMachine.GameSM;
using UnityEngine;

namespace Main.Scripts.Infrastructure.GameBootstrap
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Transform _npcContainer;
        
        private GameStateMachine StateMachine { get; set; }
        
        private void Awake()
        {
            StateMachine = new GameStateMachine(AllServices.Container, _npcContainer);
            StateMachine.Enter<BootstrapState>();
        }

        private void FixedUpdate()
        {
            StateMachine?.UpdateStatePhysics();
        }

        private void Update()
        {
            StateMachine?.UpdateStateLogic();
        }
    }
}