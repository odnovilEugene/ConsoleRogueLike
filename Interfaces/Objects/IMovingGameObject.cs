using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMovingGameObject
    {
        public event Action<GameObject, Vector2> OnMove;
        Vector2 ChooseDirection();
        void Move();
    }
}