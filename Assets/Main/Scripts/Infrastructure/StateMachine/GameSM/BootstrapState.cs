using Main.Scripts.Infrastructure.Services.GameConfigService;
using Main.Scripts.Infrastructure.Services.GameDataService;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.Services.ResourceLoader;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Main.Scripts.Infrastructure.StateMachine.GameSM
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;
        private readonly Transform _npcContainer;

        public BootstrapState(GameStateMachine gameStateMachine, AllServices services, Transform npcContainer)
        {
            _stateMachine = gameStateMachine;
            _services = services;
            _npcContainer = npcContainer;
        }
        
        public void Enter()
        {
            RegisterServices();
            _stateMachine.Enter<LevelLoadState>();
        }
        
        public void Exit()
        {
            
        }
        
        private void RegisterServices()
        {
            RegisterInputService();
            RegisterGameDataService();
            RegisterResourceService();
            RegisterConfigService();
            RegisterGameFactory();
        }

        private void RegisterInputService()
        {
            _services.RegisterSingle<IInputService>(new StandaloneInputService());
        }

        private void RegisterGameDataService()
        {
            _services.RegisterSingle<IGameDataService>(new GameDataService());
        }

        private void RegisterConfigService()
        {
            _services.RegisterSingle<IGameConfigService>(new GameConfigService(AllServices.Container.Single<IResourceLoader>()));
        }

        private void RegisterResourceService()
        {
            _services.RegisterSingle<IResourceLoader>(new ResourceLoader());
        }

        private void RegisterGameFactory()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory(AllServices.Container.Single<IResourceLoader>(), _npcContainer));
        }
    }
}
