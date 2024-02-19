using RogueLike.Components.Core;

namespace RogueLike.Interfaces.Objects
{
    public interface IGameObject
    {
        public Vector2 Position { get; set; }
        public static char Symbol { get; }
    }
}