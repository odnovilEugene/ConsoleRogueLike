using RogueLike.Components.Core;

namespace RogueLike
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Реструктурировать файлы, только Program вверху
            
            Game.Instance.GameLoop();
        }
    }
}