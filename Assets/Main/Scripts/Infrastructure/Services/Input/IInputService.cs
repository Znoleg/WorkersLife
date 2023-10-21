using Main.Scripts.Infrastructure.Services.ServiceLocator;
using UnityEngine;

namespace Main.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 GetMovementAxis(out bool isPressing);
        bool InteractClicked();
    }
}
