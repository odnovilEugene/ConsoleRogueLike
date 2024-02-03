using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Shooter : GameObject, IMovingGameObject, ILivingGameObject
    {
        public int MaxHp { get; }
        public int Hp { get; private set; }
        public int Attack { get; }
        public bool IsDead { get => Hp <= 0; }

        public Shooter(Position2D pos)
        {
            Position = pos;
            Symbol = 'S';
            MaxHp = 1;
            Hp = MaxHp;
            Attack = 0;
        }

        public void Move(Position2D pos) {}

        public void TakeDamage(int amount)
        {
            Hp -= amount;   
        }

        public override string ToString()
        {
            return Symbol.ToString();
        }

        public string GetInfo()
        {
            var className = GetType().Name;
            return $"{className}: Hp {Hp} / {MaxHp}, Position: {Position}";
        }
    }
}