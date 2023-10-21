using Main.Scripts.Infrastructure.BehaviourTree;
using UnityEngine;

namespace Main.Scripts.NpcAi
{
    public class TaskData
    {
        public Crate TargetObject;
        public Vector2 TargetPosition;
    }

    public class WaitForInteraction : Node
    {
        private readonly float _defaultWaitingTime;
        private float _waitingTime;
        private readonly Npc _npc;
        private readonly float _minWaitingTime;
        private readonly float _maxWaitingTime;

        public WaitForInteraction(float minWaitingTime, float maxWaitingTime, Npc npc)
        {
            _minWaitingTime = minWaitingTime;
            _maxWaitingTime = maxWaitingTime;
            _npc = npc;
            ResetTime();
        }
        
        public override NodeState Evaluate()
        {
            if (_npc.TaskData != null)
            {
                _npc.CanInteract = false;
                State = _npc.TaskGiven ? NodeState.Success : NodeState.Failure;
                ResetTime();
                return State;
            }
            
            _waitingTime -= Time.deltaTime;
            if (_waitingTime <= 0f)
            {
                _npc.CanInteract = false;
                State = NodeState.Failure;
                ResetTime();
                return State;
            }
            
            _npc.CanInteract = true;
            State = NodeState.Running;
            return State;
        }
        
        private void ResetTime()
        {
            _waitingTime = Random.Range(_minWaitingTime, _maxWaitingTime);
        }
    }
}
