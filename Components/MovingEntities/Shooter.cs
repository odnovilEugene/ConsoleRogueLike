using RogueLike.Components.Core;
using RogueLike.Components.StaticObjects;
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
            Symbol = Settings.ObjectSymbols.ShooterSymbol;
            MaxHp = 1;
            Hp = MaxHp;
            Attack = 0;
            Game.OnTurn += Move;
        }

        public (int, int) ChooseDirection()
        {
            Position2D playerPos = Game.Instance.Player.Position;
            (int dx, int dy) = (0, 0);
            if (Position.Y == playerPos.Y)
            {
                (dx, dy) = Position.X - playerPos.X > 0 ? (-1, 0) : (1, 0);
                Position2D tempPos = Position;
                Position2D tempPlayerPos = playerPos;
                do
                {
                    tempPos.X += dx;
                    if (Map.Instance[tempPos] is not Empty)
                        return (0, 0);
                } while (tempPos != tempPlayerPos);
                return (dx, dy);
            }
            else if (Position.X == playerPos.X)
            {
                (dx, dy) = Position.Y - playerPos.Y > 0 ? (0, -1) : (0, 1);
                Position2D tempPos = Position;
                Position2D tempPlayerPos = playerPos;
                do
                {
                    tempPos.Y += dy;
                    if (Map.Instance[tempPos] is not Empty)
                        return (0, 0);
                } while (tempPos != tempPlayerPos);
                return (dx, dy);
            }
            return (dx, dy);
        }

        public void Move() 
        {
            Console.WriteLine("Shooters move activated");
            (int dx, int dy) = ChooseDirection();
            if ((dx, dy) != (0, 0))
            {
                Position2D newPos = new(Position.X + dx, Position.Y + dy);
                var objectOnCell = Map.Instance[newPos];
                switch (objectOnCell)
                {
                    case IStaticGameObject staticObject:
                        if (staticObject.IsPassable)
                        {
                            if (staticObject is FirstAidKit aidKit)
                                aidKit.SelfDestruct();
                            else
                            {
                                Map.Instance[newPos] = new Projectile(newPos, (dx, dy));
                            }
                        }
                        break;
                    case ILivingGameObject livingGameObject:
                        livingGameObject.TakeDamage(Attack);
                        break;
                }
            }
        }

        public void TakeDamage(int amount)
        {
            Hp -= amount;
            if (IsDead)
            {
                Game.Instance.Enemies.Remove(Position);
                Map.Instance[Position] = new Empty(Position);
            } 
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