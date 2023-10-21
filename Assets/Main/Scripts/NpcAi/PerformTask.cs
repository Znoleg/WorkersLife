using Main.Scripts.Infrastructure.BehaviourTree;

namespace Main.Scripts.NpcAi
{
    public class PerformTask : Node
    {
        private readonly Npc _npc;
        private bool _gotObject;
        
        public PerformTask(Npc npc)
        {
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            TaskData taskData = _npc.GetTask();
            if (taskData == null)
            {
                return NodeState.Failure;
            }

            if (!_gotObject)
            {
                _npc.MoveTo(taskData.TargetObject.transform.position);
                if (_npc.IsMoving)
                {
                    return State = NodeState.Running;
                }
                
                taskData.TargetObject.Attach(_npc.transform);
                _gotObject = true;
            }
            
            _npc.MoveTo(taskData.TargetPosition);
            if (_npc.IsMoving)
            {
                return State = NodeState.Running;
            }

            _gotObject = false;
            taskData.TargetObject.Detach();
            _npc.ClearTask();
            
            return State = NodeState.Success;
        }
    }
}