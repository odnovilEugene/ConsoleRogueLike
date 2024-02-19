using RogueLike.Components.Core;

namespace RogueLike.Settings
{
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