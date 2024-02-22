using RogueLike.Components.Core;
using RogueLike.Components.ObjectProps;
using RogueLike.Components.StaticObjects;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Player : GameObject, ILivingGameObject
    {
        public int Attack { get; }
        public bool IsDead { get => Health.Hp <= 0; }
        public Health Health { get; private set; }

        public Player(Vector2 pos)
        {
            Position = pos;
            Symbol = Settings.ObjectSymbols.PlayerSymbol;
            Health = new(10, 10);
            Attack = 2;
            OnMove += Map.Instance.MoveGameObject;
        }

        public event Action<GameObject>? OnDeath;
        public event Action<GameObject, Vector2>? OnMove;

        public void Move(Vector2 direction) 
        {
            Vector2 newPos = new(Position + direction);
            var objectOnCell = Map.Instance[newPos];
            switch (objectOnCell)
            {
                case IStaticGameObject staticObject:
                    if (staticObject.IsPassable)
                    {
                        switch(staticObject)
                        {
                            case FirstAidKit aidKit:
                                Heal(aidKit.HealAmount);
                                break;
                            case Exit exit:
                                Game.Instance.LevelDone = true;
                                break;
                        }                            
                        ChangePosition(newPos);
                    }
                    break;
                case ILivingGameObject livingGameObject:
                    livingGameObject.TakeDamage(Attack);
                    if (livingGameObject.IsDead)
                    {
                        ChangePosition(newPos);
                    }
                    break;
                case Projectile projectile:
                    projectile.BlowUp();
                    ChangePosition(newPos);
                    break;
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
        }

        private void Heal(int amount)
        {
            Health += amount;
        }

        public string GetInfo()
        {
            var className = GetType().Name;
            return $"{className}: Hp {Health}, Position: {Position}";
        }

        public void Die() {}
        public void ChangePosition(Vector2 newPosition) 
        {
            OnMove?.Invoke(this, newPosition);
        }
    }
}