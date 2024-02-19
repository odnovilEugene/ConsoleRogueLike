using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

using static RogueLike.Settings.ObjectSymbols;

namespace RogueLike.Components.StaticObjects
{
    public class Empty : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        

        public Empty(Vector2 pos)
        {
            Position = pos;
            Symbol = EmptyCellSymbol;
            IsPassable = true;
        }
    }
}