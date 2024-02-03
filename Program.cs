using RogueLike.Components.Core;

namespace RogueLike
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Реструктурировать файлы, только Program вверху
            
            int width = 25;
            int height = 15;

            var game = new Game(height, width);
            game.RenderGame();
        }
    }
}