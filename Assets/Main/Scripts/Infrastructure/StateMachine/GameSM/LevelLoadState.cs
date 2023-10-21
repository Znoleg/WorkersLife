using System.Threading.Tasks;
using Cinemachine;
using Main.Scripts.Data;
using Main.Scripts.Gui;
using Main.Scripts.Infrastructure.Services.GameConfigService;
using Main.Scripts.Infrastructure.Services.GameDataService;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Main.Scripts.NpcAi;
using Main.Scripts.Player;
using Main.Scripts.Player.SM;
using UnityEngine;

namespace Main.Scripts.Infrastructure.StateMachine.GameSM
{
    public class LevelLoadState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public LevelLoadState(GameStateMachine gameStateMachine, AllServices allServices)
        {
            _stateMachine = gameStateMachine;
            _services = allServices;
        }
        
        public async void Enter()
        {
            InitHud();
            await InitPlayer();
            await CreateNpcs();
            _stateMachine.Enter<GameLoopState>();
        }
        
        public void Exit()
        {
            
        }

        private async Task CreateNpcs()
        {
            IGameFactory gameFactory = _services.Single<IGameFactory>();
            IGameConfigService configService = _services.Single<IGameConfigService>();
            WorldConfig worldConfig = configService.WorldConfig;
            
            int npcCount = Random.Range(worldConfig.MinNpcCount, worldConfig.MaxNpcCount);

            for (int i = 0; i < npcCount; i++)
            {
                const float npcSpawnRange = 5f;
                const float crateSpawnRange = 3f;
                
                Vector2 npcPosition = Random.insideUnitCircle * npcSpawnRange;
                Npc npc = await gameFactory.CreateNpc(npcPosition);
                Vector2 cratePosition = npcPosition + Random.insideUnitCircle * crateSpawnRange;
                Crate crate = await gameFactory.CreateCrate(cratePosition);

                npc.Init(crate, configService.NpcConfig);
            }
        }

        private async Task InitPlayer()
        {
            IGameFactory gameFactory = _services.Single<IGameFactory>();
            PlayerTasker player = await gameFactory.CreatePlayer(Vector2.zero);
            PlayerStateMachine playerStateMachine = player.GetComponent<PlayerStateMachine>();
            playerStateMachine.Init(_services.Single<IInputService>());
            CinemachineVirtualCamera cinemachineCamera = Object.FindObjectOfType<CinemachineVirtualCamera>();
            cinemachineCamera.Follow = player.transform;
        }

        private void InitHud()
        {
            DayTimeData dayTimeData = _services.Single<IGameDataService>().GameData.DayTimeData;
            
            DayImagePresenter dayImagePresenter = Object.FindObjectOfType<DayImagePresenter>();
            dayImagePresenter.Init(dayTimeData);

            DayTimeSliderPresenter dayTimeSliderPresenter = Object.FindObjectOfType<DayTimeSliderPresenter>();
            dayTimeSliderPresenter.Init(dayTimeData);
        }
    }
}