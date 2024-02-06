using RogueLike.Components.Core;
using RogueLike.Interfaces.Objects;

namespace RogueLike.Components.Render
{
    public class Renderer
    {
        public static void PrintGame()
        {
            Console.Clear();
            Console.WriteLine($"Level : {Game.Instance.Level}");
            Console.WriteLine(Map.Instance);
            
            // for(int i = 0; i < 3; i++)
            // {
            //     for (int j = 0; j < 3; j++)
            //     {
            //         Console.Write(Map.Instance[j, i]);
            //     }
            //     Console.WriteLine();
            // }
        }
        public static void PrintInfo()
        {
            Console.WriteLine(Game.Instance.Player.GetInfo());
            foreach (KeyValuePair<Position2D, ILivingGameObject> enemy in Game.Instance.Enemies)
            {
                Console.WriteLine(enemy.Value.GetInfo());
            }
        }
    }
}