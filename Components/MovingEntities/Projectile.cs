using RogueLike.Components.Core;
using RogueLike.Components.StaticObjects;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Projectile : GameObject, IMovingGameObject
    {
        private int Damage { get; }
        private Vector2 Direction { get; }

        public Projectile(Vector2 pos, Vector2 direction)
        {
            Position = pos;
            Symbol = Settings.ObjectSymbols.ProjectileSymbol;
            Damage = 1;
            Direction = direction;
            Game.OnTurn += Move;
            OnBlowUp += Map.Instance.RemoveGameObject;
            OnMove += Map.Instance.MoveGameObject;
        }

        public event Action<GameObject>? OnBlowUp;
        public event Action<GameObject, Vector2>? OnMove;

        public void BlowUp()
        {
            OnBlowUp?.Invoke(this);
            Game.OnTurn -= Move;
            OnBlowUp -= Map.Instance.RemoveGameObject;
            OnMove -= Map.Instance.MoveGameObject;
        }

        public Vector2 ChooseDirection() => Direction;

        public void Move() {
            Vector2 direction = ChooseDirection();
            Vector2 newPos = new(Position + direction);
            var objectOnCell = Map.Instance[newPos];
            switch (objectOnCell)
            {
                case IStaticGameObject staticObject:
                    if (staticObject.IsPassable)
                    {
                        if (staticObject is FirstAidKit aidKit)
                        {
                            aidKit.SelfDestruct();
                            this.BlowUp();
                        }
                        else
                        {
                            OnMove?.Invoke(this, newPos);
                            Position = newPos;
                        }
                    }
                    else
                    {
                        this.BlowUp();
                    }
                    break;
                case ILivingGameObject livingGameObject:
                    livingGameObject.TakeDamage(Damage);
                    this.BlowUp();
                    break;
                case Projectile projectile:
                    projectile.BlowUp();
                    this.BlowUp();
                    break;
            }
        }
    }
}