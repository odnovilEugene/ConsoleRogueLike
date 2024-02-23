using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;

namespace RogueLike.Components.StaticObjects
{
    public class FirstAidKit : GameObject, IStaticGameObject
    {
        public bool IsPassable { get; }

        public int HealAmount { get; }

        public FirstAidKit(Vector2 pos, int healAmount = 5)
        {
            Position = pos;
            // Symbol = Game.Instance.SymbolContainer.GetFirstAidKitChar;
            Symbol = ObjectSymbols.FirstAidKitSymbol;
            IsPassable = true;
            HealAmount = healAmount;
        }

        public void SelfDestruct()
        {
            Map.Instance.RemoveGameObject(this);
        }
    }
}