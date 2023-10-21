using UnityEngine;

namespace Main.Scripts.Player.SM
{
    public interface IMovable
    {
        void Move(Vector2 moveDir);
    }
}