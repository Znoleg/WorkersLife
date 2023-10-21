using System.Collections.Generic;
using Main.Scripts.Data;
using Main.Scripts.Infrastructure.BehaviourTree;
using Main.Scripts.Infrastructure.Services.GameFactory;
using Main.Scripts.Infrastructure.Services.ServiceLocator;
using Tree = Main.Scripts.Infrastructure.BehaviourTree.Tree;

namespace Main.Scripts.NpcAi
{
    public class NpcBehaviourTree : Tree
    {
        public NpcBehaviourTree(Npc npc, NpcConfig npcConfig) : base(new Selector(new List<Node> {
            new Sequence(new List<Node> {
                new WaitForInteraction(npcConfig.MinTaskWaitTime, npcConfig.MaxTaskWaitTime, npc),
                new WaitForPlayerTaskEnd(AllServices.Container.Single<IGameFactory>().Player, npc)
            }),
            new PerformTask(npc) }))
        {
            
        }
    }
}
