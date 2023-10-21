using Main.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.NpcAi
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        void Interact(Transform interactor);
    }
    
    public class Crate : MonoBehaviour, IInteractable
    {
        private Transform _initialParent;
        private readonly Vector3 _attachPoint = new Vector3(0f, 1f, 0f);
        private bool _attached;

        public bool CanInteract { get; set; }
        public bool Attached => _attached;

        private void Awake()
        {
            _initialParent = transform.parent;
        }

        public void Interact(Transform interactor)
        {
            if (interactor.TryGetComponent(out PlayerTasker playerTasker))
            {
                Attach(interactor);
            }
        }

        public void Attach(Transform attachTo)
        {
            _attached = true;
            transform.SetParent(attachTo);
            transform.localPosition = _attachPoint;
        }

        public void Detach()
        {
            _attached = false;
            transform.SetParent(_initialParent);
        }
    }
}