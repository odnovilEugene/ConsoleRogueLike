using RogueLike.Components.Core;
using RogueLike.Interfaces.Settings;

namespace RogueLike.Settings
{
    public class SymbolContainer : ISymbolContainer
    {
        public char GetPlayerChar => 'P';
        public char GetZombieChar => 'Z';
        public char GetShooterChar => 'S';
        public char GetProjectileChar => '*';
        public char GetEmptyChar => ' ';
        public char GetWallChar => '▓';
        public char GetExitChar => 'X';
        public char GetFirstAidKitChar => '+';
    }
    
    internal static class ObjectSymbols
    {
        internal const char EmptyCellSymbol = ' ';
        internal const char WallSymbol = '▓';
        internal const char ExitSymbol = 'E';
        internal const char FirstAidKitSymbol = '+';
        internal const char PlayerSymbol = 'P';
        internal const char ZombieSymbol = 'Z';
        internal const char ShooterSymbol = 'S';
        internal const char ProjectileSymbol = '*';
    }

    internal static class MapSettings
    {
        internal const int Height = 15;
        internal const int Width = 25;
        internal const int Seed = -1;
        internal static Vector2 start = new(1, 1);
        internal static Vector2 finish = new(Width - 2, Height - 2);
    }
}