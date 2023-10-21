using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.Input;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Main.Scripts.NpcAi;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class InteractChecker : MonoBehaviour
    {
        [SerializeField] private float _interactRange;
        
        private IGameFactory _gameFactory;
        private IInputService _inputService;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Update()
        {
            if (CanInteract(out IInteractable closest))
            {
                if (closest.CanInteract && _inputService.InteractClicked())
                {
                    closest.Interact(transform);
                }
            }
        }

        private bool CanInteract(out IInteractable closest)
        {
            IEnumerable<MonoBehaviour> behaviours = Enumerable.Empty<MonoBehaviour>().Concat(_gameFactory.Npcs).Concat(_gameFactory.Crates);
            MonoBehaviour closestBehaviour = GetClosest(behaviours);
            closest = null;
            if (closestBehaviour != null)
            {
                closest = closestBehaviour.GetComponent<IInteractable>();
            }

            return closest != null;
        }

        private T GetClosest<T>(IEnumerable<T> enumerable) where T : MonoBehaviour
        {
            return enumerable.Select(behaviour => (behaviour, distance: Vector2.Distance(transform.position, behaviour.transform.position)))
                .Where(x => x.distance <= _interactRange).OrderBy(x => x.distance).FirstOrDefault().behaviour;
        }
        
        private bool InRange(Transform target)
        {
            return Vector2.Distance(transform.position, target.position) <= _interactRange;
        }
    }
}
