using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Player : GameObject, IMovingGameObject, ILivingGameObject
    {
        public int MaxHp { get; }
        public int Hp { get; private set; }
        public int Attack { get; }
        public bool IsDead { get => Hp <= 0; }

        public Player(Position2D pos)
        {
            Position = pos;
            Symbol = 'P';
            MaxHp = 10;
            Hp = MaxHp;
            Attack = 2;
        }

        public void Move(Position2D pos) {}

        public void TakeDamage(int amount)
        {
            Hp -= amount;
        }

        public void Heal(int amount)
        {
            int newHp = Hp + amount;
            Hp = MaxHp <= newHp ? MaxHp : newHp;
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