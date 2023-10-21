using Main.Scripts.Infrastructure.BehaviourTree;
using Main.Scripts.Player;

namespace Main.Scripts.NpcAi
{
    public class WaitForPlayerTaskEnd : Node
    {
        private readonly PlayerTasker _playerTasker;
        private readonly Npc _npc;

        public WaitForPlayerTaskEnd(PlayerTasker playerTasker, Npc npc)
        {
            _playerTasker = playerTasker;
            _npc = npc;
        }
        
        public override NodeState Evaluate()
        {
            if (_playerTasker.TaskStatus.InProcess)
            {
                return State = NodeState.Running;
            }

            _npc.ClearTask();            
            return State = NodeState.Success;
        }
    }
}