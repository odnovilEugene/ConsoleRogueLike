using RogueLike.Components;
using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IMap : IReadOnlyMap
    {
        public Vector2 Start { get; set; }
        public Vector2 Finish { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        void GenerateField(int width, int height, Vector2 start, Vector2 finish);
        void SetParams(int width, int height, Vector2 start, Vector2 finish);
        void MoveGameObject(GameObject obj, Vector2 pos);
        void RemoveGameObject(GameObject obj);
        void AddGameObject(GameObject obj);
    }

    public interface IReadOnlyMap
    {
        GameObject[,] Field { get; }
        MazeGenerator MazeGenerator { get; }
    }
}