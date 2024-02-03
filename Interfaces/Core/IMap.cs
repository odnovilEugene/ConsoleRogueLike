using RogueLike.Components;
using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMap : IReadOnlyMap
    {
        int Seed { get; set; }
    }

    public interface IReadOnlyMap
    {
        int Height { get; }
        int Width { get; }
        MazeGenerator MazeGenerator { get; }
        GameObject[,] Field { get; }
    }
}