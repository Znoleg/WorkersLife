using Main.Scripts.Data;
using Main.Scripts.Infrastructure.Services.ServiceLocator;

namespace Main.Scripts.Infrastructure.Services.GameDataService
{
    public interface IGameDataService : IService
    {
        public GameData GameData { get; }
    }
}