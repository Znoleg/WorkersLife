using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Services.ResourceLoader;

namespace Main.Scripts.Infrastructure.Services.GameConfigService
{
    public class GameConfigService : IGameConfigService
    {
        public GameConfigService(IResourceLoader resourceLoader)
        {
            DayTimeConfigHolder configHolder = resourceLoader.Load<DayTimeConfigHolder>("Data/DayTimeConfig");
            DayTimeConfig = configHolder.Config;
            NpcConfigHolder npcConfigHolder = resourceLoader.Load<NpcConfigHolder>("Data/NpcConfig");
            NpcConfig = npcConfigHolder.Config;
            WorldConfigHolder worldConfigHolder = resourceLoader.Load<WorldConfigHolder>("Data/WorldConfig");
            WorldConfig = worldConfigHolder.Config;
        }
        
        public DayTimeConfig DayTimeConfig { get; }
        public NpcConfig NpcConfig { get; }
        public WorldConfig WorldConfig { get; }
    }
}