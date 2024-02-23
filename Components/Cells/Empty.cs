using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;

namespace RogueLike.Components.StaticObjects
{
    public class Empty : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        

        public Empty(Vector2 pos)
        {
            Position = pos;
            // Symbol = Game.Instance.SymbolContainer.GetEmptyChar;
            Symbol = ObjectSymbols.EmptyCellSymbol;
            IsPassable = true;
        }
    }
}