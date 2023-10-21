using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        
        public Vector2 GetMovementAxis(out bool isPressing)
        {
            Vector2 axis = UnityAxis();
            isPressing = axis != Vector2.zero;
            return axis;
        }

        public bool InteractClicked()
        {
            return UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }

        private static Vector2 UnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxisRaw(HorizontalAxis), UnityEngine.Input.GetAxisRaw(VerticalAxis));
        }
    }
}