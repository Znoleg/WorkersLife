using UnityEngine;

namespace Main.Scripts.Data
{
    [CreateAssetMenu(menuName = "Create DayTimeConfigHolder", fileName = "DayTimeConfigHolder", order = 0)]
    public class DayTimeConfigHolder : ScriptableObject
    {
        public DayTimeConfig Config;
    }
}