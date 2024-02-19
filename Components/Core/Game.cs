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

        public Dictionary<Vector2, ILivingGameObject> Enemies { get; } = new();
        private bool IsGameOver => Player.IsDead;
        private bool LevelDone => Enemies.Count == 0;        

        // Значение по умолчанию
        public Game()
        {
            Player = new Player(MapSettings.start);
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
            int enemiesCount = 0;
            while (enemiesCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for enemies");
                    break;
                }
                Vector2 enemyPos = Vector2.GetRandom(xR, yR);

                if (Map.Instance[enemyPos] is Empty)
                {
                    Enemies.Add(enemyPos, (Random.Shared.Next(0, 100) % 2 == 0) ? new Zombie(enemyPos) : new Shooter(enemyPos));
                    Map.Instance[enemyPos] = (GameObject)Enemies[enemyPos];
                    enemiesCount++;
                }
                counter++;
            }
        }

        public void RemoveEnemy(GameObject obj)
        {
            Enemies.Remove(obj.Position);
        }

        private void GenerateProps(int n, Range xR, Range yR)
        {
            int counter = 0;
            int propsCount = 0;
            while (propsCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for props");
                    break;
                }
                Vector2 propPos = Vector2.GetRandom(xR, yR);
                if (Map.Instance[propPos] is Empty)
                {
                    Map.Instance[propPos] = new FirstAidKit(propPos);
                    propsCount++;
                }
                counter++;
            }
        }


        private Vector2 LevelLoop()
        {
            Vector2 direction;
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
            Initialize();
            Vector2 direction;
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

        private void MakeTurn(Vector2 direction)
        {   
            Player.Move(direction);
            OnTurn?.Invoke();
        }
    }
}