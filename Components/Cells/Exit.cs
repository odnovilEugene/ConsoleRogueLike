using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;

namespace RogueLike.Components.StaticObjects
{
    public class Exit : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }
        
        public Exit(Vector2 pos)
        {
            Position = pos;
            // Symbol = Game.Instance.SymbolContainer.GetExitChar;
            Symbol = ObjectSymbols.ExitSymbol;
            IsPassable = true;
        }
    }
}