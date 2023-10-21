using System.Collections.Generic;
using System.Threading.Tasks;
using Main.Scripts.Infrastructure.Services.ResourceLoader;
using Main.Scripts.NpcAi;
using Main.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly List<Npc> _npcs = new List<Npc>();
        private readonly List<Crate> _crates = new List<Crate>();
        private readonly Transform _npcContainer;
        private SpriteRenderer _hintRenderer;

        public IReadOnlyList<Npc> Npcs => _npcs;
        public IReadOnlyList<Crate> Crates => _crates;
        public PlayerTasker Player { get; private set; }

        public GameFactory(IResourceLoader resourceLoader, Transform npcContainer)
        {
            _resourceLoader = resourceLoader;
            _npcContainer = npcContainer;
        }

        public async Task<Npc> CreateNpc(Vector2 at)
        {
            GameObject npcObject = await _resourceLoader.LoadAsync<GameObject>("Prefabs/Npc");
            GameObject npcInstance = Object.Instantiate(npcObject, _npcContainer);
            Npc npc = npcInstance.GetComponent<Npc>();
            npc.transform.position = at;
            _npcs.Add(npc);
            return npc;
        }

        public async Task<Crate> CreateCrate(Vector2 at)
        {
            GameObject crateObject = await _resourceLoader.LoadAsync<GameObject>("Prefabs/Crate");
            GameObject crateInstance = Object.Instantiate(crateObject, _npcContainer);
            Crate crate = crateInstance.GetComponent<Crate>();
            crate.transform.position = at;
            _crates.Add(crate);
            return crate;
        }

        public async Task<PlayerTasker> CreatePlayer(Vector2 at)
        {
            GameObject playerObject = await _resourceLoader.LoadAsync<GameObject>("Prefabs/Player");
            GameObject playerInstance = Object.Instantiate(playerObject);
            PlayerTasker player = playerInstance.GetComponent<PlayerTasker>();
            player.transform.position = at;
            Player = player;
            return player;
        }

        public async Task<GameObject> GetTargetHint(Vector2 at)
        {
            if (_hintRenderer != null)
            {
                _hintRenderer.gameObject.SetActive(true);
                return _hintRenderer.gameObject;
            }
            
            GameObject hintObject = await _resourceLoader.LoadAsync<GameObject>("Prefabs/TargetHint");
            GameObject hintInstance = Object.Instantiate(hintObject);
            SpriteRenderer hintRenderer = hintInstance.GetComponent<SpriteRenderer>();
            hintInstance.transform.position = at;
            _hintRenderer = hintRenderer;
            return hintInstance;
        }

        public void ReturnTargetHint()
        {
            _hintRenderer.gameObject.SetActive(false);
        }
    }
}