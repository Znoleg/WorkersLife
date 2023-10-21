using System;

namespace Main.Scripts.Data
{
    [Serializable]
    public class DayTimeData
    {
        /// <summary>
        /// 0 = Start of the day, 0.5 = night, 1f = next day
        /// </summary>
        public float DayValue;
    }
}
