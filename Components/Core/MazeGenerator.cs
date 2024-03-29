
using RogueLike.Components.Core;
using RogueLike.Components.StaticObjects;
using static RogueLike.Utils.Utils;
using RogueLike.Settings;

namespace RogueLike.Components
{
    public class MazeGenerator
    {
        private char[,]? _data;
        private int _width;
        private int _height;
        private Random _random;

        public MazeGenerator()
        {
            _random = new Random(MapSettings.Seed != -1 ? MapSettings.Seed : (int)DateTime.Now.Ticks);
        }

        public GameObject[,] Generate(int width, int height, Vector2 start, Vector2 finish)
        {
            _width = width;
            _height = height;
            _data = new char[_width, _height];
            Initialize();
            GenerateMaze(start.X, start.Y);
            MakeAccessible(finish);
            _random = new Random((int)DateTime.Now.Ticks);
            return CharMazeToGameObjectMaze();
        }

        private GameObject[,] CharMazeToGameObjectMaze()
        {
            var gameObjectMaze = new GameObject[_width, _height];
            
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var pos = new Vector2(x, y);
                    // _data[x, y] == Game.Instance.SymbolContainer.GetWallChar
                    if (_data[x, y] == ObjectSymbols.WallSymbol)
                    {                 
                        gameObjectMaze[x, y] = new Wall(pos);
                    }
                    // _data[x, y] == Game.Instance.SymbolContainer.GetEmptyChar
                    else if (_data[x, y] == ObjectSymbols.EmptyCellSymbol)
                    {
                        gameObjectMaze[x, y] = new Empty(pos);
                    }
                }
            }
            return gameObjectMaze;
        }

        private void Initialize()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    // _data[x, y] = Game.Instance.SymbolContainer.GetWallChar;
                    _data[x, y] = ObjectSymbols.WallSymbol;
                }
            }
        }

        private void GenerateMaze(int x, int y)
        {
            _data[x, y] = ' ';
            var directions = new (int, int)[] { (0, -2), (2, 0), (0, 2), (-2, 0) };
            directions = Shuffle(directions, _random);

            foreach (var (dx, dy) in directions)
            {
                int newX = x + dx, newY = y + dy;
                // IsInsideBounds(newX, newY) && _data[newX, newY] != Game.Instance.SymbolContainer.GetEmptyChar
                if (IsInsideBounds(newX, newY) && _data[newX, newY] != ObjectSymbols.EmptyCellSymbol)
                {
                    // _data[newX - dx / 2, newY - dy / 2] = Game.Instance.SymbolContainer.GetEmptyChar;
                    _data[newX - dx / 2, newY - dy / 2] = ObjectSymbols.EmptyCellSymbol;
                    GenerateMaze(newX, newY);
                }
            }
        }

        private void MakeAccessible(Vector2 finish)
        {   
            var directions = new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) };
            foreach (var (dx, dy) in directions)
            {
                int newX = finish.X + dx, newY = finish.Y + dy;
                if (IsInsideBounds(newX, newY))
                {
                    // _data[newX, newY] = Game.Instance.SymbolContainer.GetEmptyChar;
                    _data[newX, newY] = ObjectSymbols.EmptyCellSymbol;
                }
            }
        }

        private bool IsInsideBounds(int x, int y)
        {
            return x > 0 && x < _width - 1 && y > 0 && y < _height - 1;
        }
    }
}