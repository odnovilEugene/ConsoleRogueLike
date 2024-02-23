using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;

namespace RogueLike.Components.StaticObjects
{
    public class Wall : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        

        public Wall(Vector2 pos)
        {
            Position = pos;
            // Symbol = Game.Instance.SymbolContainer.GetWallChar;
            Symbol = ObjectSymbols.WallSymbol;
            IsPassable = false;
        }

        public void Break() {}
    }
}