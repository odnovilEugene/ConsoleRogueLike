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
            // Назвать Props
            // StaticObjects = GenerateStaticObjects(Map.Width / 8);
            // Подписывать каждый проджектайл на событие, которое вызывалось бы каждый ход
            // Projectiles = new List<Projectile>();
            
        }

        private void Initialize(bool startCorner = true)
        {
            Level++; 
            int MapHeight = Map.Height;
            int MapWidth = Map.Width;

            int enemiesNumber = MapWidth / 8 + Level;
            int propNumber = (MapWidth / 8) - Level >= 1 ? (MapWidth / 8) - Level : 1;

            Player.Position = startCorner ? MapSettings.start : MapSettings.finish;
            Map.Instance[Player.Position] = Player;

            Range heightR = startCorner ? new(3, MapHeight - 1) : new(0, MapHeight - 4);
            Range widthR = startCorner ? new(3, MapWidth - 1) : new(3, MapWidth - 4);
            GenerateEnemies(enemiesNumber, heightR, widthR);
            GenerateProps(propNumber, heightR, widthR);
        }

        public static void RenderGame()
        {
            Renderer.PrintGame();
        }

        private void GenerateEnemies(int n, Range yR, Range xR)
        {
            int counter = 0;
            while (EnemiesCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for enemies");
                    break;
                }
                // Вынести в отдельные переменные либо создать отдельный метод (GetRandom), возвращаюший рандомную позицию
                // В методе использовать Range height, width и Random.Shared
                Position2D enemyPos = Position2D.GetRandom(yR, xR);
                int y = enemyPos.Y;
                int x = enemyPos.X;
                if (Map.Instance[y, x] is Empty)
                {
                    // Использовать тернарник
                    Enemies.Add(enemyPos, (Random.Shared.Next(0, 100) % 2 == 0) ? new Zombie(enemyPos) : new Shooter(enemyPos));
                    Map.Instance[y, x] = (GameObject)Enemies[enemyPos];
                    EnemiesCount++;
                }
                counter++;
            }
        }


        // Тоже, что и выше
        private void GenerateProps(int n, Range yR, Range xR)
        {
            int counter = 0;
            while (PropsCount < n)
            {
                if (counter > 100)
                {
                    Console.WriteLine("No place for enemies");
                    break;
                }
                Position2D propPos = Position2D.GetRandom(yR, xR);
                int y = propPos.Y;
                int x = propPos.X;
                if (Map.Instance[y, x] is Empty)
                {
                    // Использовать тернарник
                    Map.Instance[y, x] = new FirstAidKit(propPos);
                    PropsCount++;
                }
                counter++;
            }
        }
       

       // Сделать метод генерейт, чтобы объединить метод инициализации и UpToNextLevel
        private void UpToNextLevel()
        {
            // Level++;
            // Player.Position = new Position2D(1, 1);
            // Map = new Map(InitialDepth, InitialWidth, -1);
            // Enemies = GenerateEnemies(Map.Width / 3);
            // StaticObjects = GenerateStaticObjects(Map.Width / 8);
            // Projectiles.Clear();
        }


        private (int, int) LevelLoop()
        {
            (int, int) direction;
            RenderGame();
            do
            {
                // Отойти от консоли, сделать свою абстракцию Action, чтобы при изменении типа инпута нужно было переписывать только свою абстракцию, а не все строчки, где вызывается этот инпут
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
                    Console.Clear();
                    Console.WriteLine("You exited the game!");
                    break;
                }

                if (IsGameOver)
                {
                    Console.Clear();
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
                        Initialize(Level % 2 != 0);
                    else if (answer == PlayerInput.RejectKey)
                        break;
                }
            } while (PlayerInput.DirectionToInput(direction) != PlayerInput.BreakKey);
        }

        private void MakeTurn((int, int) direction)
        {   
            Player.Move(direction);
            OnTurn();
        }
    }
}