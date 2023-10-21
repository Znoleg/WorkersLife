using Main.Scripts.Data;

namespace Main.Scripts.Infrastructure.Services.GameDataService
{
    public class GameDataService : IGameDataService
    {
        public GameData GameData { get; }

        public GameDataService()
        {
            GameData = new GameData
            {
                DayTimeData = new DayTimeData()
                {
                    DayValue = 0f
                }
            };
        }
    }
}