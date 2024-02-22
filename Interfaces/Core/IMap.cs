using System.Collections.Immutable;
using RogueLike.Components;
using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMap : IReadOnlyMap
    {
        static int Seed { get; set; }
        static int Height { get; set; }
        static int Width { get; set; }
        GameObject[,] Field { get; set; }
    }

    public interface IReadOnlyMap
    {
        MazeGenerator MazeGenerator { get; }
    }
}