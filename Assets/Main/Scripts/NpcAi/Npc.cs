using Main.Scripts.Data;
using Main.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.NpcAi
{
    public class Npc : MonoBehaviour, IInteractable
    {
        private const float MoveEpsilon = 0.01f;
        private float _taskTargetPositionRange;
        private NpcBehaviourTree _npcTree;
        private Vector2 _endPosition;
        private Crate _crate;
        private TaskData _taskData;
        private float _speed;

        public bool IsMoving => Vector2.Distance(transform.position, _endPosition) > 0f;
        public bool CanInteract { get; set; }
        public bool TaskGiven { get; private set; }
        public TaskData TaskData => _taskData;
        
        private void Update()
        {
            _npcTree?.Evaluate();
            Move();
        }

        public void Init(Crate crate, NpcConfig npcConfig)
        {
            _endPosition = transform.position;
            _crate = crate;
            _speed = npcConfig.Speed;
            _taskTargetPositionRange = npcConfig.TaskTargetPositionRange;
            _npcTree = new NpcBehaviourTree(this, npcConfig);
        }
        
        public void Interact(Transform interactor)
        {
            if (interactor.TryGetComponent(out PlayerTasker player))
            {
                TaskData task = GetTask();
                task.TargetObject.CanInteract = true;
                TaskGiven = true;
                player.SetTask(task);
            }
        }

        public TaskData GetTask()
        {
            if (_taskData != null)
            {
                return _taskData;
            }

            TaskGiven = false;
            _taskData = new TaskData()
            {
                TargetObject = _crate,
                TargetPosition = GenerateTaskPosition()
            };

            return _taskData;
        }

        public void ClearTask()
        {
            if (_taskData != null)
            {
                _taskData.TargetObject.CanInteract = false;
            }

            TaskGiven = false;
            _taskData = null;
        }
        
        public void MoveTo(Vector2 position)
        {
            _endPosition = position;
        }

        private void Move()
        {
            if (MoveX(_speed, _endPosition, transform.position))
            {
                return;
            }

            MoveY(_speed, _endPosition, transform.position);
        }
        
        private bool MoveX(float speed, Vector2 targetPosition, Vector2 npcPosition)
        {
            float xDiff = targetPosition.x - npcPosition.x;
            float xDelta = xDiff > 0f ? speed : -speed;
            
            if (Mathf.Abs(xDiff) > MoveEpsilon)
            {
                transform.position += new Vector3(xDelta, 0f, 0f) * Time.deltaTime;
                return true;
            }

            Vector3 position = npcPosition;
            position.x = targetPosition.x;
            transform.position = position;
            return false;
        }
        
        private bool MoveY(float speed, Vector2 targetPosition, Vector2 npcPosition)
        {
            float yDiff = targetPosition.y - npcPosition.y;
            float yDelta = yDiff > 0f ? speed : -speed;
            
            if (Mathf.Abs(yDiff) > MoveEpsilon)
            {
                transform.position += new Vector3(0f, yDelta, 0f) * Time.deltaTime;
                return true;
            }

            Vector3 position = npcPosition;
            position.y = targetPosition.y;
            transform.position = position;
            return false;
        }

        private Vector2 GenerateTaskPosition()
        {
            Vector2 newPosition = (Vector2) transform.position + Random.insideUnitCircle * _taskTargetPositionRange;
            return newPosition;
        }
    }
}