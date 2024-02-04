using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.MovingGameObject
{
    public class Projectile : GameObject, IMovingGameObject
    {
        public Projectile(Position2D pos)
        {
            Position = pos;
            Symbol = Settings.ObjectSymbols.ProjectileSymbol;
            Game.OnTurn += Move;
        }
        public void Move() {}
    }
}