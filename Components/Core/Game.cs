using RogueLike.Components.MovingGameObje;
using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;
using RogueLike.Components.StaticObjects;
using RogueLike.Components.Render;
using RogueLike.Components.Constants;

// using static RogueLike.Utils.Utils;

namespace RogueLike.Components.Core
{
    public class Game
    {
        public int Height { get; }
        public int Width { get; }
        public int Seed { get; private set; }
        public int Level { get; private set; } = 0;
        public int EnemiesCount { get; set; } = 0;
        public int PropsCount { get; set; } = 0;
        public Map Map { get; private set; }
        public Player Player { get; private set; }
        // private List<Projectile> Projectiles { get; set; }
        private bool IsGameOver => Player.Hp <= 0;
        // private bool LevelDone => Enemies.Count == 0;
        

        // Значение по умолчанию
        public Game(int height, int width, int seed = -1)
        {
            Height = height;
            Width = width;
            Seed = seed != -1 ? seed : (int)DateTime.Now.Ticks;
            Player = new Player(new Position2D(1, 1));
            Initialize(Seed, true);
            
            // Назвать Props
            // StaticObjects = GenerateStaticObjects(Map.Width / 8);
            // Подписывать каждый проджектайл на событие, которое вызывалось бы каждый ход
            // Projectiles = new List<Projectile>();
            
        }

        private void Initialize(int seed, bool startCorner)
        {
            Level++;
            int EnemiesNumber = Width / 8 + Level;
            int PropNumber = (Width / 8) - Level >= 1 ? (Width / 8) - Level : 1;
            Map = new Map(Height, Width, new Position2D(1, 1), new Position2D(Height - 2, Width - 2), seed);
            if (startCorner)
            {
                Position2D startCornerPosition = Settings.start;
                Player.Position = startCornerPosition;
                Map[Player.Position] = Player;
                GenerateEnemies(EnemiesNumber, new Range(3, Map.Height - 1), new Range(3, Map.Width - 1));
                GenerateProps(PropNumber, new Range(3, Map.Height - 1), new Range(3, Map.Width - 1));
            } else
            {
                Position2D finishCornerPosition = Settings.finish;
                Player.Position = finishCornerPosition;
                Map[Player.Position] = Player;
                GenerateEnemies(EnemiesNumber, new Range(0, Map.Height - 4), new Range(3, Map.Width - 4));
                GenerateProps(PropNumber, new Range(0, Map.Height - 4), new Range(3, Map.Width - 4));
            }
        }

        public void RenderGame()
        {
            Renderer.PrintGame(this);
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
                if (Map[x, y] is Empty)
                {
                    // Использовать тернарник
                    Map[x, y] = (Random.Shared.Next(0, 100) % 2 == 0) ? new Zombie(enemyPos) : new Shooter(enemyPos);
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
                if (Map[x, y] is Empty)
                {
                    // Использовать тернарник
                    Map[x, y] = new FirstAidKit(propPos);
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


        // private ConsoleKey StartGame()
        // {
        //     ConsoleKey key;
        //     Renderer.PrintGame(this);
        //     do
        //     {
        //         // Отойти от консоли, сделать свою абстракцию Action, чтобы при изменении типа инпута нужно было переписывать только свою абстракцию, а не все строчки, где вызывается этот инпут
        //         key = Console.ReadKey().Key;
        //         MakeTurn(key);
        //         Renderer.PrintGame(this);

        //     } while ((key != ConsoleKey.Enter) && !LevelDone && !IsGameOver);
        //     return key;
        // }

        private void ProjectileMove(Projectile projectile)
        {
            
        }

        private void MakeTurn(ConsoleKey key)
        {   
            
        }
        
        private bool InBounds(Position2D pos)
        {
            return 0 < pos.Y && pos.Y <= Map.Height - 1 
            && 0 < pos.X && pos.X <= Map.Width - 1;
        }

        // Обойтись
        // public void GameLoop()
        // {   
        //     ConsoleKey key;
        //     do
        //     {
        //         key = StartGame();
        //         // Разбить на отдельные методы
        //         if (IsGameOver)
        //         {
        //             Console.Clear();
        //             Console.WriteLine("GAME OVER!");
        //             break;
        //         }

        //         if (key == ConsoleKey.Enter)
        //         {
        //             Console.Clear();
        //             Console.WriteLine("You exited the game!");
        //             break;
        //         }

        //         ConsoleKey answer;
        //         do 
        //         {
        //             Console.Clear();
        //             Console.Write("Continue to next level?\ny/n : ");
        //             answer = Console.ReadKey().Key;
        //         } while ((answer != ConsoleKey.Y) && (answer != ConsoleKey.N));

        //         if (answer == ConsoleKey.Y)
        //             UpToNextLevel();
        //         else if (answer == ConsoleKey.N)
        //             break;

        //     } while (key != ConsoleKey.Enter);
        // }
    }
}