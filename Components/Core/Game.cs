using RogueLike.Components.MovingGameObject;
using RogueLike.Components.StaticObjects;
using RogueLike.Components.Render;
using RogueLike.Settings;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.Core
{
    public sealed class Game
    {
        private static readonly Lazy<Game> lazy = new(() => new Game());

        public static Game Instance { get { return lazy.Value; } }

        public int Level { get; private set; } = 0;
        public int EnemiesCount { get; set; } = 0;
        public int PropsCount { get; set; } = 0;
        public Player Player { get; private set; }

        public static event Action? OnTurn;

        public Dictionary<Position2D, ILivingGameObject> Enemies { get; } = new();
        private bool IsGameOver => Player.Hp <= 0;
        private bool LevelDone => Enemies.Count == 0;        

        // Значение по умолчанию
        public Game()
        {
            Player = new Player(MapSettings.start);
            Initialize();
        }

        private void Initialize(bool startCorner = true)
        {
            Level++; 
            int MapHeight = Map.Height;
            int MapWidth = Map.Width;

            int enemiesNumber = MapWidth / 8 + Level;
            int propNumber = (MapWidth / 8) - Level >= 1 ? (MapWidth / 8) - Level : 1;

            Map.Instance.Field = Map.Instance.MazeGenerator.Generate(MapSettings.start, MapSettings.finish);

            Map.Instance[Player.Position] = new Empty(Player.Position);
            Player.Position = startCorner ? MapSettings.start : MapSettings.finish;
            Map.Instance[Player.Position] = Player;

            Range widthR = startCorner ? new(3, MapWidth - 1) : new(0, MapWidth - 2);
            Range heightR = startCorner ? new(3, MapHeight - 1) : new(0, MapHeight - 2);
            GenerateEnemies(enemiesNumber, widthR, heightR);
            GenerateProps(propNumber, widthR, heightR);
        }

        public static void RenderGame()
        {
            Renderer.PrintGame();
        }

        private void GenerateEnemies(int n, Range xR, Range yR)
        {
            int counter = 0;
            while (EnemiesCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for enemies");
                    break;
                }
                Position2D enemyPos = Position2D.GetRandom(xR, yR);
                int y = enemyPos.Y;
                int x = enemyPos.X;
                if (Map.Instance[x, y] is Empty)
                {
                    Enemies.Add(enemyPos, (Random.Shared.Next(0, 100) % 2 == 0) ? new Zombie(enemyPos) : new Shooter(enemyPos));
                    Map.Instance[x, y] = (GameObject)Enemies[enemyPos];
                    EnemiesCount++;
                }
                counter++;
            }
        }

        private void GenerateProps(int n, Range xR, Range yR)
        {
            int counter = 0;
            while (PropsCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for enemies");
                    break;
                }
                Position2D propPos = Position2D.GetRandom(xR, yR);
                int y = propPos.Y;
                int x = propPos.X;
                if (Map.Instance[x, y] is Empty)
                {
                    // Использовать тернарник
                    Map.Instance[x, y] = new FirstAidKit(propPos);
                    PropsCount++;
                }
                counter++;
            }
        }


        private (int, int) LevelLoop()
        {
            (int, int) direction;
            RenderGame();
            do
            {
                direction = PlayerInput.ReadMoveInput();
                MakeTurn(direction);
                RenderGame();
            } while (PlayerInput.DirectionToInput(direction) != PlayerInput.BreakKey && !LevelDone && !IsGameOver);
            
            return direction;
        }

        public void GameLoop()
        {
            (int, int) direction;
            do
            {
                direction = LevelLoop();

                if (PlayerInput.DirectionToInput(direction) == PlayerInput.BreakKey)
                {
                    Console.WriteLine("You exited the game!");
                    break;
                }

                if (IsGameOver)
                {
                    Console.WriteLine("GAME OVER!");
                    break;
                }

                if (LevelDone) {
                    ConsoleKey answer;
                    do 
                    {
                        Console.Clear();
                        Console.Write("Continue to next level?\ny/n : ");
                        answer = PlayerInput.ReadInput();
                    } while ((answer != PlayerInput.AcceptKey) && (answer != PlayerInput.RejectKey));

                    if (answer == PlayerInput.AcceptKey)
                        Initialize(Level % 2 == 0);
                    else if (answer == PlayerInput.RejectKey)
                    {
                        Console.Write("You exited the game!");
                        break;
                    }
                }
            } while (PlayerInput.DirectionToInput(direction) != PlayerInput.BreakKey);
        }

        private void MakeTurn((int, int) direction)
        {   
            Player.Move(direction);
            OnTurn?.Invoke();
        }
    }
}