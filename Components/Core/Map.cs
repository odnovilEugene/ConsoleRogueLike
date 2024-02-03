using RogueLike.Interfaces.Objects;
using RogueLike.Settings;



namespace RogueLike.Components.Core
{
    public sealed class Map : IMap
    {
        private static readonly Lazy<Map> lazy = new(() => new Map());

        public static Map Instance { get { return lazy.Value; } }

        public int Height { get; }
        public int Width { get; }
        public GameObject[,] Field { get; set; }
        public MazeGenerator MazeGenerator { get; }
        public int Seed { get; set; }

        public Map()
        {
            Height = MapSettings.Height;
            Width = MapSettings.Width;
            Seed = MapSettings.Seed != -1 ? MapSettings.Seed : (int)DateTime.Now.Ticks;
            MazeGenerator = new MazeGenerator(Width, Height, Seed);
            Field = MazeGenerator.Generate(MapSettings.start, MapSettings.finish);
        }
        
        public override string ToString()
        {
            string stringMap = "";
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    stringMap += this[y, x];
                }
                stringMap += "\n";
            }
            return string.Format(stringMap);
        }
    
        // Индексаторы для Map
        public GameObject this [Position2D pos] {
            get => Field[pos.Y, pos.X];
            set => Field[pos.Y, pos.X] = value;
        }

        public GameObject this [int y, int x] {
            get => Field[x, y];
            set => Field[x, y] = value;
        }
    }
}
