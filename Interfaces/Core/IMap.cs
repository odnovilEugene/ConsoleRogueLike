using RogueLike.Components;
using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMap : IReadOnlyMap
    {
        static int Seed { get; set; }
        static int Height { get; set; }
        static int Width { get; set; }
    }

    public interface IReadOnlyMap
    {
        MazeGenerator MazeGenerator { get; }
        GameObject[,] Field { get; }
    }
}