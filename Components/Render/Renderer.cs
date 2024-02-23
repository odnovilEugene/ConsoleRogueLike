using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.Render
{
    public static class Renderer
    {
        public static void PrintGame()
        {
            Console.Clear();
            Console.WriteLine($"Level : {Game.Instance.Level}");
            Console.WriteLine(Map.Instance);
            PrintInfo();
        }
        private static void PrintInfo()
        {
            Console.WriteLine(Game.Instance.Player.GetInfo());
            foreach (var enemy in Game.Instance.Enemies)
            {
                Console.WriteLine(enemy.Value.GetInfo());
            }
        }

        public static void PrintGameOverMsg()
        {
            Console.WriteLine("GAME OVER!");
        }

        public static void PrintLevelDoneMsg()
        {
            Console.Clear();
            Console.Write("Continue to next level?\ny/n : ");
        }

        public static void PrintGameExitMsg()
        {
            Console.WriteLine("You exited the game!");
        }
        public static void PrintGlobalExitMsg()
        {
            Console.WriteLine("You used global exit flag somehow!?");
        }
    }
}