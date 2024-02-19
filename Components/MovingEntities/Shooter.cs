using RogueLike.Components.Core;
using RogueLike.Components.ObjectProps;
using RogueLike.Components.StaticObjects;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Shooter : GameObject, IMovingGameObject, ILivingGameObject
    {
        public int Attack { get; }
        public bool IsDead { get => Health.Hp <= 0; }
        public Health Health { get; private set; }

        public Shooter(Vector2 pos)
        {
            Position = pos;
            Symbol = Settings.ObjectSymbols.ShooterSymbol;
            Health = new(1, 1);
            Attack = 1;
            Game.OnTurn += Move;
            OnDeath += Game.Instance.RemoveEnemy;
            OnDeath += Map.Instance.RemoveGameObject;
            OnShoot += Map.Instance.AddGameObject;
        }

        public event Action<GameObject>? OnDeath;
        public event Action<GameObject, Vector2>? OnMove;
        public event Action<GameObject>? OnShoot;


        public Vector2 ChooseDirection()
        {
            Vector2 playerPos = Game.Instance.Player.Position;
            Vector2 direction = (0, 0);
            if (Position.Y == playerPos.Y)
            {
                direction = Position.X - playerPos.X > 0 ? (-1, 0) : (1, 0);
                Vector2 tempPos = Position;
                Vector2 tempPlayerPos = playerPos;
                while (Math.Abs(tempPos.X - tempPlayerPos.X) > 1)
                {
                    tempPos.X += direction.X;
                    if (Map.Instance[tempPos] is not Empty)
                        return (0, 0);
                }
                return direction;
            }
            else if (Position.X == playerPos.X)
            {
                direction = Position.Y - playerPos.Y > 0 ? (0, -1) : (0, 1);
                Vector2 tempPos = Position;
                Vector2 tempPlayerPos = playerPos;
                while (Math.Abs(tempPos.Y - tempPlayerPos.Y) > 1)
                {
                    tempPos.Y += direction.Y;
                    if (Map.Instance[tempPos] is not Empty)
                        return (0, 0);
                }
                return direction;
            }
            return direction;
        }

        public void Move() 
        {
            Vector2 direction = ChooseDirection();
            if (direction != (0, 0))
            {
                Vector2 newPos = new(Position + direction);
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
                                OnShoot?.Invoke(new Projectile(newPos, direction));
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
            Health -= amount;
            if (IsDead)
            {
                OnDeath?.Invoke(this);
                Die();
            }
        }

        public string GetInfo()
        {
            var className = GetType().Name;
            return $"{className}: Hp {Health}, Position: {Position}";
        }

        public void Die()
        {
            Game.OnTurn -= Move;
            OnDeath -= Game.Instance.RemoveEnemy;
            OnDeath -= Map.Instance.RemoveGameObject;
            OnShoot += Map.Instance.AddGameObject;
        }

        public void ChangePosition(Vector2 newPosition) {}
    }
}