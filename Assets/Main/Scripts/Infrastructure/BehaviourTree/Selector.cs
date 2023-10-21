using System.Collections.Generic;

namespace Main.Scripts.Infrastructure.BehaviourTree
{
    public class Selector : Node
    {
        public Selector(IEnumerable<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (Node node in Children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        return State = NodeState.Success;
                    case NodeState.Running:
                        return State = NodeState.Running;
                    default:
                        State = NodeState.Success;
                        return State;
                }
            }

            return State = NodeState.Failure;
        }
    }
}