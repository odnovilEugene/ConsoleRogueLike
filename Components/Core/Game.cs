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
        public Player Player { get; private set; }
        public Exit Exit { get; set;}

        private string Status 
        {
            get
            {
                if (IsGameOver)
                {
                    return "GameOver";
                }
                if (LevelDone)
                {
                    return "LevelDone";
                }
                if (ReturnPressed)
                {
                    return "ReturnPressed";
                }
                return "InProgress";
            }
        }

        public static event Action? OnTurn;

        public Dictionary<Vector2, ILivingGameObject> Enemies { get; } = new();
        private bool IsGameOver => Player.IsDead;
        private bool ReturnPressed { get; set; }
        private bool _levelDone = false;

        public bool LevelDone
        {
            get => _levelDone || Enemies.Count == 0;
            set => _levelDone = value;
        }  

        // Значение по умолчанию
        public Game()
        {
            Player = new Player(MapSettings.start);
            Exit = new Exit(MapSettings.finish);
        }

        private void Initialize(bool startCorner = true)
        {
            Level++;

            ClearEnemies();

            LevelDone = false;

            int MapHeight = Map.Height;
            int MapWidth = Map.Width;

            int enemiesNumber = MapWidth / 8 + Level;
            int propNumber = (MapWidth / 8) - Level >= 1 ? (MapWidth / 8) - Level : 1;

            Map.Instance.Field = Map.Instance.MazeGenerator.Generate(MapSettings.start, MapSettings.finish);

            Vector2 playerNewPos = startCorner ? MapSettings.start : MapSettings.finish;
            Vector2 exitPos = startCorner ? MapSettings.finish : MapSettings.start;
            Map.Instance.MoveGameObject(Exit, exitPos);
            Map.Instance.MoveGameObject(Player, playerNewPos);

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
                    Map.Instance.AddGameObject((GameObject)Enemies[enemyPos]);
                    enemiesCount++;
                }
                counter++;
            }
        }

        public void ClearEnemies()
        {
            foreach(var enemy in Enemies)
            {
                enemy.Value.Die();
            }
            Enemies.Clear();
        }

        public void RemoveEnemy(GameObject obj)
        {
            Enemies.Remove(obj.Position);
        }

        public void ReassignKey(ILivingGameObject obj, Vector2 fromKey, Vector2 toKey)
        {
            Enemies.Remove(fromKey);
            Enemies.Add(toKey, obj);
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
                    Map.Instance.AddGameObject(new FirstAidKit(propPos));
                    propsCount++;
                }
                counter++;
            }
        }

        private void AskForContinue()
        {
            ConsoleKey answer;
            do 
            {
                Renderer.PrintLevelDoneMsg();
                answer = PlayerInput.ReadInput();
            } while ((answer != PlayerInput.AcceptKey) && (answer != PlayerInput.RejectKey));

            if (answer == PlayerInput.AcceptKey)
            {
                Initialize(Level % 2 == 0);
            }
            else if (answer == PlayerInput.RejectKey)
            {
                Renderer.PrintGameExitMsg();
                return;
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
                ReturnPressed = PlayerInput.DirectionToInput(direction) == PlayerInput.BreakKey;

                switch (Status)
                {
                    case "GameOver":
                        Renderer.PrintGameOverMsg();
                        return;
                    case "LevelDone":
                        AskForContinue();
                        break;
                    case "ReturnPressed":
                        Renderer.PrintGameExitMsg();
                        return;
                }
            } while (!ReturnPressed);
        }

        private void MakeTurn(Vector2 direction)
        {   
            Player.Move(direction);
            OnTurn?.Invoke();
        }
    }
}