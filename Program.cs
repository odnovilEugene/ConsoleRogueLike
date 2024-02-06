using System.Runtime.CompilerServices;
using RogueLike.Components.Core;
using RogueLike.Settings;

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