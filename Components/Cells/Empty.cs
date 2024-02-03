using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

using static RogueLike.Components.Constants.Constants;

namespace RogueLike.Components.StaticObjects
{
    public class Empty : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        

        public Empty(Position2D pos)
        {
            Position = pos;
            Symbol = EmptyCellSymbol;
            IsPassable = true;
        }
    }
}