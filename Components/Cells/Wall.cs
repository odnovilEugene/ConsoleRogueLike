using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

using static RogueLike.Components.Constants.Constants;

namespace RogueLike.Components.StaticObjects
{
    public class Wall : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        

        public Wall(Position2D pos)
        {
            Position = pos;
            Symbol = WallSymbol;
            IsPassable = false;
        }

        public void Break() {}
    }
}