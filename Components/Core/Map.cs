using RogueLike.Components.StaticObjects;
using RogueLike.Interfaces.Objects;
using RogueLike.Settings;



namespace RogueLike.Components.Core
{
    public sealed class Map : IMap
    {
        private static readonly Lazy<Map> lazy = new(() => new Map());

        public static Map Instance { get { return lazy.Value; } }

        public Vector2 Start { get; set; }
        public Vector2 Finish { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        private GameObject[,] _field;

        public GameObject[,] Field
        {
            get => (GameObject[,])_field.Clone();
            private set => _field = value;
        }
        public MazeGenerator MazeGenerator { get; }

        public Map()
        {
            Height = MapSettings.Height;
            Width = MapSettings.Width;
            Start = MapSettings.start;
            Finish = MapSettings.finish;
            MazeGenerator = new MazeGenerator();
        }

        public void GenerateField(int width, int height, Vector2 start, Vector2 finish)
        {
            SetParams(width, height, start, finish);
            Field = MazeGenerator.Generate(Width, Height, Start, Finish);
        }

        public void SetParams(int width, int height, Vector2 start, Vector2 finish)
        {
            Width = width;
            Height = height;
            Start = start;
            Finish = finish;
        }

        public void MoveGameObject(GameObject obj, Vector2 pos)
        {
            this[obj.Position] = new Empty(obj.Position);
            this[pos] = obj;
            obj.Position = pos;
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
        public GameObject this [Vector2 pos] 
        {
            get => Field[pos.X, pos.Y];
            private set
            {
                _field[pos.X, pos.Y] = value;
            }
        }

        public GameObject this [int x, int y] {
            get => Field[x, y];
            private set
            {
                _field[x, y] = value;
            }
        }
    }
}
