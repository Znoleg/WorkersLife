using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Services.ServiceLocator;

namespace Main.Scripts.Infrastructure.Services.GameConfigService
{
    public interface IGameConfigService : IService
    {
        public DayTimeConfig DayTimeConfig { get; }
        public NpcConfig NpcConfig { get; }
        public WorldConfig WorldConfig { get; }
    }
}