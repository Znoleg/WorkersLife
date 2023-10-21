using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence(IEnumerable<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        State = NodeState.Failure;
                        return State;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        State = NodeState.Running;
                        return State;
                    default:
                        State = NodeState.Success;
                        return State;
                }
            }

            return State = NodeState.Success;
        }
    }
}