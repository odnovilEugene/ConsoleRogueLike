using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;



namespace RogueLike.Components.Core
{
    // public sealed class Map : IMap
    // {
    //     private static readonly Lazy<Map> lazy = new Lazy<Map>(() => new Map());

    //     public static Map Instance { get { return lazy.Value; } }

    //     public int Height { get; }
    //     public int Width { get; }
    //     public IGameObject[,] Field { get; }
    //     public MazeGenerator MazeGenerator { get; }
    //     public int Seed { get; set; }

    //     public Map(int height, int width, Position2D start, Position2D finish, int seed)
    //     {
    //         Height = height;
    //         Width = width;
    //         Seed = seed;
    //         MazeGenerator = new MazeGenerator(Width, Height, Seed);
    //         Field = MazeGenerator.Generate(start, finish);
    //     }
        
    //     public override string ToString()
    //     {
    //         string stringMap = "";
    //         for (int y = 0; y < Height; y++)
    //         {
    //             for (int x = 0; x < Width; x++)
    //             {
    //                 stringMap += this[x, y];
    //             }
    //             stringMap += "\n";
    //         }
    //         return string.Format(stringMap);
    //     }
    
    //     // Индексаторы для Map
    //     public IGameObject this [Position2D pos] {
    //         get => Field[pos.Y, pos.X];
    //         set => Field[pos.Y, pos.X] = value;
    //     }

    //     public IGameObject this [int y, int x] {
    //         get => Field[y, x];
    //         set => Field[y, x] = value;
    //     }
    // }

    public class Map : IMap
    {
        public int Height { get; }
        public int Width { get; }
        public IGameObject[,] Field { get; }
        public MazeGenerator MazeGenerator { get; }
        public int Seed { get; set; }

        public Map(int height, int width, Position2D start, Position2D finish, int seed)
        {
            Height = height;
            Width = width;
            Seed = seed;
            MazeGenerator = new MazeGenerator(Width, Height, Seed);
            Field = MazeGenerator.Generate(start, finish);
        }
        
        public override string ToString()
        {
            string stringMap = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    stringMap += this[x, y];
                }
                stringMap += "\n";
            }
            return string.Format(stringMap);
        }
    
        // Индексаторы для Map
        public IGameObject this [Position2D pos] {
            get => Field[pos.Y, pos.X];
            set => Field[pos.Y, pos.X] = value;
        }

        public IGameObject this [int y, int x] {
            get => Field[y, x];
            set => Field[y, x] = value;
        }
    }
}
