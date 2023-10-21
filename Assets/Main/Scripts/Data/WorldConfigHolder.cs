using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "Create WorldConfigHolder", fileName = "WorldConfig", order = 0)]
    public class WorldConfigHolder : ScriptableObject
    {
        public WorldConfig Config;
    }
}