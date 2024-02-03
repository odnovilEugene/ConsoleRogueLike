using RogueLike.Components.Core;

namespace RogueLike.Components.Render
{
    public class Renderer
    {
        public static void PrintGame(Game game)
        {
            var map = Map.Instance;
            Console.Clear();
            Console.WriteLine($"Level : {game.Level}");
            Console.WriteLine(map);
            game.Player.GetInfo();
            // TODO: понять как выводить информацию о противниках не храня ее в отдельном списке
            // 1: Пробегать по всей карте и если это объект, который двигается - добавлять информацию о нем в массив с инфой в этом компоненте
            // PrintInfo(player, enemies);
        }
        // public static void PrintInfo(Player player, List<IMovingGameObject> enemies)
        // {
        //     Console.WriteLine(player.GetInfo());
        //     foreach (var enemy in enemies)
        //     {
        //         Console.WriteLine(enemy.GetInfo());
        //     }
        // }
    }
}