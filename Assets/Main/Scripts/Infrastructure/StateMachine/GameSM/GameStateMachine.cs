using Main.Scripts.Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Main.Scripts.Infrastructure.StateMachine.GameSM
{
    public class GameStateMachine : BaseStateMachine
    {
        public GameStateMachine(AllServices allServices, Transform npcContainer)
        {
            AddState(typeof(BootstrapState), new BootstrapState(this, allServices, npcContainer));
            AddState(typeof(LevelLoadState), new LevelLoadState(this, allServices));
            AddState(typeof(GameLoopState), new GameLoopState(allServices));
        }
    }
}