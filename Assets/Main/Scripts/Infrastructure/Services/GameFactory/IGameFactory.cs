using System.Collections.Generic;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Main.Scripts.NpcAi;
using Main.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameFactory
{
    public interface IGameFactory : IService
    {
        IReadOnlyList<Npc> Npcs { get; }
        IReadOnlyList<Crate> Crates { get; }
        PlayerTasker Player { get; }
        Task<Npc> CreateNpc(Vector2 at);
        Task<Crate> CreateCrate(Vector2 at);
        Task<PlayerTasker> CreatePlayer(Vector2 at);
        Task<GameObject> GetTargetHint(Vector2 at);
        void ReturnTargetHint();
    }
}
