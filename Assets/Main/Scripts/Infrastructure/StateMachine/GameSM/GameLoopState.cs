using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Services.GameConfigService;
using Main.Scripts.Infrastructure.Services.GameDataService;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Main.Scripts.Infrastructure.StateMachine.GameSM
{
    public class GameLoopState : IState, IPhysicsUpdatableState
    {
        private readonly AllServices _allService;
        private DayTimeData _dayTimeData;
        private DayTimeConfig _dayTimeConfig;
        private float _step;

        public GameLoopState(AllServices allServices)
        {
            _allService = allServices;
        }
        
        public void Exit()
        {
            
        }

        public void Enter()
        {
            IGameDataService gameDataService = _allService.Single<IGameDataService>();
            _dayTimeData = gameDataService.GameData.DayTimeData;
            IGameConfigService gameConfigService = _allService.Single<IGameConfigService>();
            _dayTimeConfig = gameConfigService.DayTimeConfig;
            _step = CalculateStep();
        }

        public void UpdatePhysics()
        {
            _dayTimeData.DayValue += _step;
        }

        private float CalculateStep()
        {
            float framesCount = _dayTimeConfig.DayCircleInSeconds / Time.fixedDeltaTime;
            float step = 1f / framesCount;
            return step;
        }
    }
}