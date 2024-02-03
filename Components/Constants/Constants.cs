using RogueLike.Components.Core;

namespace RogueLike.Components.Constants
{
    internal static class Constants
    {
        internal const char EmptyCellSymbol = ' ';
        internal const char WallSymbol = '▓';
        internal const char FirstAidKitSymbol = '+';
    }

    internal static class Settings
    {
        internal const int Height = 15;
        internal const int Width = 25;
        internal const int Seed = -1;
        internal static Position2D start = new(1, 1);
        internal static Position2D finish = new(Height - 2, Width - 2);
    }
}