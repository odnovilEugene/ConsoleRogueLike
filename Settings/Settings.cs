using RogueLike.Components.Core;

namespace RogueLike.Settings
{
    internal static class ObjectSymbols
    {
        internal const char EmptyCellSymbol = ' ';
        internal const char WallSymbol = '▓';
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
        internal static Position2D start = new(1, 1);
        internal static Position2D finish = new(Height - 2, Width - 2);
    }
}