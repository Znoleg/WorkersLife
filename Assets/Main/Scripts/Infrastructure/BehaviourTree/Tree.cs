namespace Main.Scripts.Infrastructure.BehaviourTree
{
    public abstract class Tree
    {
        private readonly Node _root = null;

        public Tree(Node root)
        {
            _root = root;
        }

        public NodeState Evaluate()
        {
            return _root.Evaluate();
        }
    }
}