using System;

namespace Main.Scripts.Data
{
    [Serializable]
    public class NpcConfig
    {
        public float Speed = 1f;
        public float TaskTargetPositionRange;
        public float MinTaskWaitTime = 3f;
        public float MaxTaskWaitTime = 7f;
    }
}