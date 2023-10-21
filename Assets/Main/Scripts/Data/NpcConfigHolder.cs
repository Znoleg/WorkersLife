using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "Create NpcConfigHolder", fileName = "NpcConfig", order = 0)]
    public class NpcConfigHolder : ScriptableObject
    {
        public NpcConfig Config;
    }
}