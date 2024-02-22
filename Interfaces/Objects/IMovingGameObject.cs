using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMovingGameObject : IGameObject
    {
        public event Action<GameObject, Vector2> OnMove;
        Vector2 ChooseDirection();
        void Move();
    }
}