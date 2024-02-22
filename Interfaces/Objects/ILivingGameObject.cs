using RogueLike.Components.Core;
using RogueLike.Components.ObjectProps;

namespace RogueLike.Interfaces.Objects
{
    public interface ILivingGameObject : IGameObject
    {
        public event Action<GameObject>? OnDeath;
        public Health Health { get; }
        public int Attack { get; }
        public bool IsDead { get; }
        void TakeDamage(int amount);
        void ChangePosition(Vector2 newPosition);
        void Die();
        string GetInfo();
    }
}