using System.Runtime.Remoting;
using RogueLike.Components.StaticObjects;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;



namespace RogueLike.Components.Core
{
    public sealed class Map : IMap
    {
        private static readonly Lazy<Map> lazy = new(() => new Map());

        public static Map Instance { get { return lazy.Value; } }
        public static int Height { get; } = MapSettings.Height;
        public static int Width { get; } = MapSettings.Width;
        public GameObject[,] Field { get; set; }
        public MazeGenerator MazeGenerator { get; }

        public Map()
        {
            MazeGenerator = new MazeGenerator(Width, Height);
            Field = MazeGenerator.Generate(MapSettings.start, MapSettings.finish);
        }

        public void MoveGameObject(GameObject obj, Vector2 pos)
        {
            this[obj.Position] = new Empty(obj.Position);
            this[pos] = obj;
        }

        public void RemoveGameObject(GameObject obj)
        {
            this[obj.Position] = new Empty(obj.Position);
        }

        public void AddGameObject(GameObject obj)
        {
            this[obj.Position] = obj;
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
        public GameObject this [Vector2 pos] {
            get => Field[pos.X, pos.Y];
            set => Field[pos.X, pos.Y] = value;
        }

        public GameObject this [int x, int y] {
            get => Field[x, y];
            set => Field[x, y] = value;
        }
    }
}
